using JopApplication.Core.DTOs.Auth;
using JopApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<string?> GenerateJwtTokenAsync(ApplicationUser user);

        public Task<string?> login(LoginDto loginDto);

        public Task<string?> register(RegisterDto registerDTO, string role);




    }
}
