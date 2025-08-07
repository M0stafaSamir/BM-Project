using JopApplication.Core.DTOs.Auth;
using JopApplication.Core.Interfaces.Services;
using JopApplication.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Services
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager ,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string?> login(LoginDto LoginUser)
        {

             var user = await _userManager.FindByEmailAsync(LoginUser.Email);
            if (user !=null)
            {
                if (await _userManager.CheckPasswordAsync(user, LoginUser.Password))
                {
                    return await GenerateJwtTokenAsync(user);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public async Task<string?> register(RegisterDto registerDTO, string role)
        {
         var user = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (user != null)
            {
                return "ExistingUser";
            }
            var newUser = new ApplicationUser
            {
                Fname = registerDTO.FName,
                Lname = registerDTO.LName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNo,
            };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, role);
                return await GenerateJwtTokenAsync(newUser);
            }
            else
            {
                return null;
            }
        }

        public async Task<string?> GenerateJwtTokenAsync(ApplicationUser user)
        {
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,$"{user.Fname} {user.Lname}"),
                    new Claim(ClaimTypes.Email, user.Email),
                    
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(3),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return null;
            }
        }

   
    }
}
