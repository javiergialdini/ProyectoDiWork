namespace ProyectoDiWork.Modelos
{
    /// <summary>
    /// Presupuesto de la reparacion
    /// </summary>
    public class Presupuesto
    {
        /// <summary>
        /// Id del presupuesto
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido del cliente
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Correo electronico del cliente
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// Total del presupuesto
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// id del vehiculo
        /// </summary>
        public int idVehiulo { get; set; }
    }
}
