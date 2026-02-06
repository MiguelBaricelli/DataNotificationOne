using DataNotificationOne.Application.Dtos.AuthSecurity;
using DataNotificationOne.Application.Interfaces;
using DataNotificationOne.Application.Services.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DataNotificationOne.Controllers.V1.AuthController
{
    [ApiController]
    [Route("v1/auth/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult GetToken([FromHeader] string apiKey)
        {

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return BadRequest();
            }
            if (!_authService.ValidateApiKey(apiKey))
                return Unauthorized("API Key inválida");

            var token = _authService.GenerateToken();
            return Ok(token);

        }
    }
}
