using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    /// <summary>
    /// Clase Vehiculo
    /// </summary>
    public abstract class Vehiculo
    {
        /// <summary>
        /// Id del vehiculo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Marca del vehiculo
        /// </summary>
        [Required]
        public string Marca { get; set; }
        /// <summary>
        /// Modelo del vehiculo
        /// </summary>
        [Required] 
        public string Modelo { get; set; }
        /// <summary>
        /// Patente de vehiculo
        /// </summary>
        [Required]
        public string Patente { get; set; }
    }

    /// <summary>
    /// Clase Automovil. Hereda de clase Vehiculo
    /// </summary>
    public class Automovil : Vehiculo
    {
        /// <summary>
        /// id del vehiculo
        /// </summary>
        public int IdAutomovil { get; set; }
        /// <summary>
        /// Tipo de automovil
        /// </summary>
        [Required]
        public int Tipo { get; set; }
        /// <summary>
        /// Cantidad de puertas del automovil
        /// </summary>
        [Required]
        public int CantidadPuertas { get; set; }
    }

    /// <summary>
    /// Clase Moto. Hereda de clase Vehiculo
    /// </summary>
    public class Moto : Vehiculo
    {
        /// <summary>
        /// Id de moto
        /// </summary>
        public int IdMoto { get; set; }
        /// <summary>
        /// Cilindrada moto
        /// </summary>
        [Required]
        public int Cilindrada { get; set; }
    }
}
