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

    /// <summary>
    /// Para contabilizar el repuesto mas utilizado por Modelo
    /// </summary>
    public class RepuestoPorModelo
    {
        /// <summary>
        /// Modelo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Id del repuesto
        /// </summary>
        public int IdRepuesto { get; set; }
        /// <summary>
        /// Nombre del repuesto
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Precio del repuesto
        /// </summary>
        public decimal Cantidad { get; set; }
    }

    /// <summary>
    /// Para contabilizar el repuesto mas utilizado por Marca
    /// </summary>
    public class RepuestoPorMarca
    {
        /// <summary>
        /// Modelo
        /// </summary>
        public string Marca { get; set; }
        /// <summary>
        /// Id del repuesto
        /// </summary>
        public int IdRepuesto { get; set; }
        /// <summary>
        /// Nombre del repuesto
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Precio del repuesto
        /// </summary>
        public decimal Cantidad { get; set; }
    }
}
