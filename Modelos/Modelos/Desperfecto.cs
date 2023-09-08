using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos
{
    /// <summary>
    /// Desperfecto diagnosticado
    /// </summary>
    public class Desperfecto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del presupuesto al cual fue asignado
        /// </summary>
        public int idPresupuesto { get; set; }
        /// <summary>
        /// Descripcion del desperfecto
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Monto de mano de obra
        /// </summary>
        public decimal ManoDeObra { get; set; }
        /// <summary>
        /// Tiempo en días de trabajo
        /// </summary>
        public int Tiempo { get; set; }
        /// <summary>
        /// Lisa de repuestos
        /// </summary>
        public List<Repuesto> Repuestos { get; set; }
    }
}
