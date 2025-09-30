using Intituicao.Financeira.Application.Shared.Domain.Entities;
using Intituicao.Financeira.Application.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security;

namespace Intituicao.Financeira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        private SecureString _secret = new NetworkCredential("", configuration.GetSection("Authentication:Secret").Value).SecurePassword;

        [HttpGet("token")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetToken([FromQuery] AuthRequest request)
        {
            var authorized = TokenService.Validate(request);

            if (!authorized)
                return Unauthorized();

            var token = TokenService.GenerateToken(_secret);

            return Ok(new AuthResponse
            {
                AccessToken = token,
                TokenType = "Bearer"
            });
        }
    }
}
