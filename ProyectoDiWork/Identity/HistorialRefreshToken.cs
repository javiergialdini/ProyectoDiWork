namespace ProyectoDiWork.Identity
{
    /// <summary>
    /// HistorialRefreshToken
    /// </summary>
    public class HistorialRefreshToken
    {
        /// <summary>
        /// IdHistorialToken
        /// </summary>
        public int IdHistorialToken { get; set; }
        /// <summary>
        /// IdUsuario
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// FechaExpiracion
        /// </summary>
        public DateTime FechaExpiracion { get; set; }
        /// <summary>
        /// EsActivo
        /// </summary>
        public bool EsActivo { get; set; }
    }
}
