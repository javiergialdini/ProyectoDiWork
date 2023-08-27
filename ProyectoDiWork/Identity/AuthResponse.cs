namespace ProyectoDiWork.Identity
{
    /// <summary>
    /// AuthResponse
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Resultado
        /// </summary>
        public bool Resultado { get; set; }
        /// <summary>
        /// Msg
        /// </summary>
        public string Msg { get; set; }
    }
}
