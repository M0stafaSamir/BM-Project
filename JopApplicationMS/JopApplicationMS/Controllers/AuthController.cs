using JopApplication.Core.DTOs.Auth;
using JopApplication.Core.Interfaces.Services;
using JopApplication.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace JopApplicationMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto RegisterUser)
        {
      

            var token = await _authService.register(RegisterUser, "User");
            if (token == "ExistingUser")
            {
                return BadRequest(ApiResponse<string>.FailResponse("Email already exist"));

            }
            else if (token != null)
            {
                return Ok(ApiResponse<string>.SuccessResponse(token, "Registerd Successfully"));
            }
            else
            {
                return BadRequest(ApiResponse<string>.FailResponse("Registeration Faild"));
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto LoginUser)
        {
            var token = await _authService.login(LoginUser);

            if (token != null)
            {
                return Ok(ApiResponse<string>.SuccessResponse(token, "Login Successfully"));
            }
            else
            {
                return BadRequest(ApiResponse<string>.FailResponse("Wrong Email or Password"));
            }

        }



    }
}
