using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ProyectoDiWork.Identity;
using ProyectoDiWork.Identity.Services;

namespace ProyectoDiWork.Controllers
{
    /// <summary>
    /// AutenticacionController
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAuthService _autorizationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autorizationService"></param>
        public AutenticacionController(IAuthService autorizationService)
        {
            _autorizationService = autorizationService;
        }

        /// <summary>
        /// Valida un usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Validar")]
        public async Task<IActionResult> Validar([FromBody] CustomUserStore request)
        {
            var resultAuth = await _autorizationService.GetAuthorizationAsync(request);
            if (resultAuth == null)
                return Unauthorized();

            return Ok(resultAuth);
        }

        /// <summary>
        /// Get refresh Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ObtenerRefreshToken")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpirado = tokenHandler.ReadJwtToken(request.TokenExpirado);

            if (tokenExpirado.ValidTo > DateTime.UtcNow)
                return BadRequest(new AuthResponse() { Resultado=false, Msg="Token no ha expirado"});

            string idUsuario = tokenExpirado.Claims.First(x => 
            x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

            var authResponse = await _autorizationService.GetRefreshToken(request, int.Parse(idUsuario));

            if (authResponse.Resultado)
                return Ok(authResponse);
            else
                return BadRequest(authResponse);
        }
    }
}
