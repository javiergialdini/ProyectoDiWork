namespace ProyectoDiWork.Modelos
{
    /// <summary>
    /// Repuesto utilizado para resolver el desperfecto
    /// </summary>
    public class Repuesto
    {
        /// <summary>
        /// Id del repuesto
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del repuesto
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Precio del repuesto
        /// </summary>
        public decimal Precio { get; set; }
    }
}
