namespace ProyectoDiWork.Identity.Services
{
    /// <summary>
    /// IAuthorizationService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// GetAuthorizationAsync
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<AuthResponse> GetAuthorizationAsync(CustomUserStore user);
        /// <summary>
        /// GetAuthorizationAsync
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<AuthResponse> GetRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario);
    }
}
