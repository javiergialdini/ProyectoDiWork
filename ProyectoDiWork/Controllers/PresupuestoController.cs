using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
    [Authorize]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private IMemoryCache _cache;
        private readonly IConverter _pdfConverter;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="pdfConverter"></param>
        public PresupuestoController([FromServices] IMemoryCache cache, [FromServices] IConverter pdfConverter)
        {
            _cache = cache;
            _pdfConverter = pdfConverter;
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
            
            if(respuesta == null)
                return BadRequest("Presupuesto no encontrado");
            

            return Ok(respuesta);
        }

        /// <summary>
        /// Obtiene presupuesto mediante Id
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        [HttpGet("Detalle")]
        [ProducesResponseType(typeof(PresupuestoDetalle), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetallePresupesto([FromQuery] int? presupuestoId = null, [FromQuery] int? vehiculoId = null)
        {
            if (presupuestoId == null && vehiculoId == null)
                return BadRequest("id del presupuesto o vehiculo requerido");

            PresupuestoDetalle respuesta = new PresupuestoDetalle();


            try
            {
                respuesta = await PresupuestoBL.ObtenerPresupuestoDetalle(presupuestoId, vehiculoId);
            }
            catch
            {
                return BadRequest("Presupuesto no encontrado");
            }

            return Ok(respuesta);
        }

        /// <summary>
        /// Descarga PDF Presupuesto
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        [HttpGet("DetallePDF")]
        [ProducesResponseType(typeof(PresupuestoDetalle), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetallePDFPresupesto([FromQuery] int? presupuestoId = null, [FromQuery] int? vehiculoId = null)
        {
            if (presupuestoId == null && vehiculoId == null)
                return BadRequest("id del presupuesto o vehiculo requerido");

            MemoryStream respuesta = new MemoryStream();

            try
            {
                respuesta = await PresupuestoBL.GenerarPdfPresupesto(presupuestoId, vehiculoId);
            }
            catch
            {
                return BadRequest("Presupuesto no encontrado");
            }

            return File(respuesta, "application/pdf", "tabla.pdf");
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

        /// <summary>
        /// Calcula el promedio total de presupuestos por marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        [HttpGet("PromedioTotalMarca")]
        [ProducesResponseType(typeof(PreTotalMarcaModelo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromedioTotalPorMarca([FromQuery] string marca)
        {
            if (marca == null || marca == "")
                return BadRequest("Marca requerida");

            PreTotalMarcaModelo respuesta = new PreTotalMarcaModelo();

            respuesta = await PresupuestoBL.ObtenerPromedioTotalPorMarca(marca);

            if (respuesta == null)
                return BadRequest("Sin registro");

            return Ok(respuesta);
        }

        /// <summary>
        /// Calcula el promedio total de presupuestos por modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpGet("PromedioTotalModelo")]
        [ProducesResponseType(typeof(PreTotalMarcaModelo), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPromedioTotalPorModelo([FromQuery] string modelo)
        {
            if (modelo == null || modelo == "")
                return BadRequest("Modelo requerido");

            PreTotalMarcaModelo respuesta = new PreTotalMarcaModelo();

            respuesta = await PresupuestoBL.ObtenerPromedioTotalPorModelo(modelo);

            if (respuesta == null)
                return BadRequest("Sin registro");

            return Ok(respuesta);
        }

        /// <summary>
        /// Sumatoria del Monto Total de Presupuestos para Autos y para Motos
        /// </summary>
        /// <returns></returns>
        [HttpGet("TotalesAutosMotos")]
        [ProducesResponseType(typeof(Dictionary<string, decimal>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalesAutosMotos()
        {
            Dictionary<string, decimal> respuesta = new Dictionary<string, decimal>();

            respuesta = await PresupuestoBL.ObtenerTotalesAutosMotos();

            if (respuesta == null)
                return BadRequest("Sin registro");

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
