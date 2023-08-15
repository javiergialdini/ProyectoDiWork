using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProyectoDiWork.Funciones;
using ProyectoDiWork.Modelos;

namespace ProyectoDiWork.Controllers
{
    /// <summary>
    /// PresupuestoController
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private IMemoryCache _cache;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        public PresupuestoController([FromServices] IMemoryCache cache)
        {
            _cache = cache;
        }

        #region LECTURA
        /// <summary>
        /// Obtiene presupuesto mediante Id
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(Presupuesto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPresupesto([FromQuery] int? presupuestoId = null, [FromQuery] int? vehiculoId = null)
        {
            if (presupuestoId == null && vehiculoId == null)
                return BadRequest("id del presupuesto o vehiculo requerido");

            Presupuesto respuesta = new Presupuesto();

            respuesta = await PresupuestoBL.ObtenerPresupuesto(presupuestoId, vehiculoId);

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene presupuesto mediante Id
        /// </summary>
        /// <param name="vehiculoIds">Se listan todos los presupuesto en caso de lista de ids vacía</param>
        /// <returns></returns>
        [HttpPost("Listar")]
        [ProducesResponseType(typeof(List<Presupuesto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarPresupuestosVehiculos([FromBody] List<int> vehiculoIds)
        {
            List<Presupuesto> respuesta = new List<Presupuesto>();

            respuesta = await PresupuestoBL.ListarPresupuestos(vehiculoIds);

            return Ok(respuesta);
        }

        #endregion

        #region ESCRITURA

        /// <summary>
        /// Carga el trabajo de un automovil
        /// </summary>
        /// <param name="trabajoAutomovil"></param>
        /// <returns></returns>
        [HttpPost("CargarTrabajoAutomovil")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CargarTrabajoAutomovil([FromBody] TrabajoAutomovilNuevo trabajoAutomovil)
        {
            if (trabajoAutomovil.Automovil == null || !FuncionesComunesBL.CheckVehiDatos(trabajoAutomovil.Automovil))
                return BadRequest("Datos de automovil requeridos");

            if(trabajoAutomovil.Presupuesto == null || !FuncionesComunesBL.CheckClienteDatos(trabajoAutomovil.Presupuesto))
                return BadRequest("Datos del cliente requeridos");


            bool trabajoCargado = await PresupuestoBL.CargarTrabajoVehiculo(trabajoAutomovil);

            return Ok(trabajoCargado);
        }

        /// <summary>
        /// Carga el trabajo de una Moto
        /// </summary>
        /// <param name="trabajoMoto"></param>
        /// <returns></returns>
        [HttpPost("CargarTrabajoMoto")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CargarTrabajoMoto([FromBody] TrabajoMotoNuevo trabajoMoto)
        {
            if (trabajoMoto.Moto == null || !FuncionesComunesBL.CheckVehiDatos(null,trabajoMoto.Moto))
                return BadRequest("Datos de automovil requeridos");

            if (trabajoMoto.Presupuesto == null || !FuncionesComunesBL.CheckClienteDatos(trabajoMoto.Presupuesto))
                return BadRequest("Datos del cliente requeridos");


            bool trabajoCargado = await PresupuestoBL.CargarTrabajoVehiculo(null, trabajoMoto);

            return Ok(trabajoCargado);
        }
        #endregion
    }
}
