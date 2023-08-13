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
