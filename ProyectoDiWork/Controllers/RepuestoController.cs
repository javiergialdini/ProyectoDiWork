using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProyectoDiWork.Funciones;
using ProyectoDiWork.Modelos;

namespace ProyectoDiWork.Controllers
{
    /// <summary>
    /// RepuestoController
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoController : ControllerBase
    {
        private IMemoryCache _cache;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        public RepuestoController([FromServices] IMemoryCache cache)
        {
            _cache = cache;
        }

        #region LECTURA

        /// <summary>
        /// Obtiene el repuesto mas utilizado para el modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpGet("Modelo/MasUtilizado")]
        [ProducesResponseType(typeof(RepuestoPorModelo), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerMayorPorModelo(string modelo)
        {
            if (modelo == "")
                return BadRequest("Modelo requerido");

            RepuestoPorModelo respuesta = await RepuestoBL.ObtenerMayorPorModelo(modelo);

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el repuesto mas utilizado para el modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpGet("Marca/MasUtilizado")]
        [ProducesResponseType(typeof(RepuestoPorMarca), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerMayorPorMarca(string marca)
        {
            if (marca == "")
                return BadRequest("Marca requerido");

            RepuestoPorMarca respuesta = await RepuestoBL.ObtenerMayorPorMarca(marca);

            return Ok(respuesta);
        }

        #endregion

        #region ESCRITURA

        /// <summary>
        /// Ejecuta el sp MassiveCharge y retorna los registros con precio >= 20. ATENCIÓN: Elimina los registros de la tabla de repuesto y carga nuevamente los del stored procedure
        /// </summary>
        /// <returns>retornia lista de Repuestos del sp con precio >= 20</returns>
        [HttpPost("MassiveCharge")]
        [ProducesResponseType(typeof(List<Repuesto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExecMassiveCharge()
        {
            List<Repuesto> repuestos = await RepuestoBL.ExecSpMassiveCharge();

            return Ok(repuestos);
        }
        #endregion
    }
}
