using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProyectoDiWork.Funciones;
using ProyectoDiWork.Modelos;
using System.Diagnostics;

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
        /// Genera lista de repuestos
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listar")]
        [ProducesResponseType(typeof(List<Repuesto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarRepuestos()
        {
            List<Repuesto> respuesta = new List<Repuesto>();

            string idCache = "repuestosCache";

            if(!_cache.TryGetValue(idCache, out respuesta))
            {
                respuesta = await RepuestoBL.ListarRepuestos();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(4));
                cacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(8));
                cacheEntryOptions.Size = 1;

                if(respuesta != null && respuesta.Count() > 0)
                {
                    try
                    {
                        // Save data in cache.
                        _cache.Set(idCache, respuesta, cacheEntryOptions);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al generar cache de repuestos: " + ex.Message);
                    }
                }

            }

            return Ok(respuesta);
        }


        /// <summary>
        /// Obtiene el repuesto mas utilizado para el modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpGet("ModeloVehi/MasUtilizado")]
        [ProducesResponseType(typeof(RepuestoPorModelo), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerMayorPorModelo(string modelo)
        {
            if (modelo == "")
                return BadRequest("Modelo requerido");

            RepuestoPorModelo respuesta = await RepuestoBL.ObtenerMayorPorModelo(modelo);

            return Ok(respuesta);
        }

        /// <summary>
        /// Lista de repuesto mas utilizado por modelo
        /// </summary>
        /// <returns></returns>
        [HttpGet("ModeloVehi/ListarMasUtilizado")]
        [ProducesResponseType(typeof(List<RepuestoPorModelo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarMayorPorModelo()
        {
            List<RepuestoPorModelo> respuesta = await RepuestoBL.ListarMayorPorModelo();

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el repuesto mas utilizado para el modelo
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        [HttpGet("MarcaVehi/MasUtilizado")]
        [ProducesResponseType(typeof(RepuestoPorMarca), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerMayorPorMarca(string marca)
        {
            if (marca == "")
                return BadRequest("Marca requerido");

            RepuestoPorMarca respuesta = await RepuestoBL.ObtenerMayorPorMarca(marca);

            return Ok(respuesta);
        }

        /// <summary>
        /// Lista de repuesto mas utilizado por marca
        /// </summary>
        /// <returns></returns>
        [HttpGet("MarcaVehi/ListarMasUtilizado")]
        [ProducesResponseType(typeof(List<RepuestoPorMarca>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarMayorPorMarca()
        {
            List<RepuestoPorMarca> respuesta = await RepuestoBL.ListarMayorPorMarca();

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
