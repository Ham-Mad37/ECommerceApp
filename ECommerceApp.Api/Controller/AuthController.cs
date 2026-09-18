using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Api.Services.Interfaces;
using Microsoft.Identity.Client.NativeInterop;
using ECommerceApp.Api.DTOs.Users;

namespace ECommerceApp.Api.Controller
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync((dto));
            if (result == null)
                return BadRequest("Email already exisit.");
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.Login((dto));
            if (result == null)
                return Unauthorized("Invalid email or password.");
            return Ok(result);
        }
    }
}