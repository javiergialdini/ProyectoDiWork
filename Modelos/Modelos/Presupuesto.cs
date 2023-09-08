using System.ComponentModel.DataAnnotations;

namespace Modelos.Modelos
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
        /// <summary>
        /// Lista de desperfectos
        /// </summary>
        public List<Desperfecto> Desperfectos { get; set; }
    }

    /// <summary>
    /// Clase para cargar trabajo para un automovil
    /// </summary>
    public class TrabajoAutomovilNuevo
    {
        /// <summary>
        /// Presupuesto
        /// </summary>
        public Presupuesto Presupuesto { get; set; }
        /// <summary>
        /// Datos del automovil
        /// </summary>
        public Automovil Automovil { get; set; }
    }

    /// <summary>
    /// Clase para cargar un trabaj a una moto
    /// </summary>
    public class TrabajoMotoNuevo
    {
        /// <summary>
        /// Presupuesto
        /// </summary>
        public Presupuesto Presupuesto { get; set; }
        /// <summary>
        /// Datos de la moto
        /// </summary>
        public Moto Moto { get; set; }
    }

    /// <summary>
    /// Clase para calcular Promedio monto total por Marca/Modelo
    /// </summary>
    public class PreTotalMarcaModelo
    {
        /// <summary>
        /// Marca
        /// </summary>
        public string Marca { get; set; }
        /// <summary>
        /// Modelo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Total
        /// </summary>
        public decimal Total { get; set; }

    }

    /// <summary>
    /// Clase para detalle de presupuesto
    /// </summary>
    public class PresupuestoDetalle : Presupuesto
    {
        /// <summary>
        /// Marca del vehiculo
        /// </summary>
        public string Marca { get; set; }
        /// <summary>
        /// Modelo del vehiculo
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Patente de vehiculo
        /// </summary>
        public string Patente { get; set; }
    }
}
