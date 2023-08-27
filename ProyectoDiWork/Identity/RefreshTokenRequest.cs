namespace ProyectoDiWork.Identity
{
    /// <summary>
    /// RefreshTokenRequest
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// Token expirado
        /// </summary>
        public string TokenExpirado { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
