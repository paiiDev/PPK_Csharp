using JWT_EncDec_Example.DTOs;
using JWT_EncDec_Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_EncDec_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("admin")]
        public IActionResult AdminLogin(LoginAdminRequestDto request)
        {
            var result = _authService.AuthenticateAdmin(request);
            if (result.IsSuccessful == false)
            {
                return Unauthorized(new { Message = "Invalid admin credentials." });
            }
            return Ok(new { Message = "Admin login successful!" });
        }
    }
}
