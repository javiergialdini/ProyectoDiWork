using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoDiWork.Identity.Services
{
    /// <summary>
    /// AuthorizationService
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly string ConexionSQL = System.Environment.GetEnvironmentVariable("ConexionSQL", EnvironmentVariableTarget.Process);
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("ApiAuth:SecretKey"); 
            var keyByte = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyByte),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = tokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            var createdToken = tokenHandler.WriteToken(tokenConfig);

            return createdToken;
        }

        /// <summary>
        /// GenerarRefreshToken
        /// </summary>
        /// <returns></returns>
        private string GenerarRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        /// <summary>
        /// DevolverToken
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private async Task<AuthResponse> GuardarHistorialRefreshToken(int idUsuario, string token, string refreshToken)
        {
            var historialRefreshToken = new HistorialRefreshToken()
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddHours(24)
            };

            bool guardado = false;
            await Task.Run(() => guardado = UserDB.SaveHistorialRefreshToken(historialRefreshToken));

            if (guardado)
                return new AuthResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "OK" };
            else
                return null;
        }

        /// <summary>
        /// GetAuthorizationAsync
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AuthResponse> GetAuthorizationAsync(CustomUserStore user)
        {
            var UserFound = UserDB.GetUser(user);

            if(UserFound == null)
            {
                return await Task.FromResult<AuthResponse>(null);
            }

            string tokenCreado = GenerarToken(UserFound.IdUsuario.ToString());

            string refreshTokenCreado = GenerarRefreshToken();

            return await GuardarHistorialRefreshToken(UserFound.IdUsuario, tokenCreado, refreshTokenCreado);
        }

        /// <summary>
        /// GetRefreshToken
        /// </summary>
        /// <param name="refreshTokenRequest"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AuthResponse> GetRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
        {
            var refreshTokenEncontrado = UserDB.GetRefreshToken(refreshTokenRequest, idUsuario);

            if(refreshTokenEncontrado == null)
                return new AuthResponse() { Resultado = false, Msg="No existe refresh token"};

            var refreshTokenCreado = GenerarRefreshToken();
            var tokenCreado = GenerarToken(idUsuario.ToString());

            return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado);
        }
    }
}
