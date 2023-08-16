using ProyectoDiWork.DataBase;
using ProyectoDiWork.Modelos;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ProyectoDiWork.Funciones
{
    /// <summary>
    /// PresupuestoBL
    /// </summary>
    public class PresupuestoBL
    {
        #region LECTURA
        /// <summary>
        /// Obtiene presupuesto mediante id
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        public static async Task<Presupuesto> ObtenerPresupuesto(int? presupuestoId = null, int? vehiculoId = null)
        {
            Presupuesto resultado = new Presupuesto();

            await Task.Run(() => 
            {
                resultado = PresupuestoDB.spPresupuestoObtener(presupuestoId, vehiculoId);
            });

            return resultado;
        }

        /// <summary>
        /// Obtiene detalle de presupuesto mediante id
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        public static async Task<PresupuestoDetalle> ObtenerPresupuestoDetalle(int? presupuestoId = null, int? vehiculoId = null)
        {
            PresupuestoDetalle resultado = new PresupuestoDetalle();

            await Task.Run(() =>
            {
                resultado = PresupuestoDB.spPresupuestoDetalleObtener(presupuestoId, vehiculoId);
            });

            if (resultado == null)
            {
                throw new Exception();
            }

            //Le sumo el 10% de recargo
            resultado.Total = resultado.Total * 1.10m;

            return resultado;
        }

        /// <summary>
        /// Obtiene detalle de presupuesto mediante id
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="vehiculoId"></param>
        /// <returns></returns>
        public static async Task<MemoryStream> GenerarPdfPresupesto(int? presupuestoId = null, int? vehiculoId = null)
        {
            PresupuestoDetalle resultado = new PresupuestoDetalle();

            await Task.Run(() =>
            {
                resultado = PresupuestoDB.spPresupuestoDetalleObtener(presupuestoId, vehiculoId);
            });

            if(resultado == null)
            {
                throw new Exception();
            }

            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 12, XFontStyle.Regular);

            gfx.DrawString("DETALLE DE PRESUPUESTO", font, XBrushes.Black, new XRect(40, 40, page.Width, page.Height), XStringFormats.TopLeft);

            int startX = 40;
            int startY = 80;
            int cellWidth = 250; // Ajusta el ancho de la celda para acomodar ambos títulos
            int cellHeight = 30;
            // Encabezado de la tabla
            for (int col = 0; col < 2; col++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, startX + col * cellWidth, startY, cellWidth, cellHeight);
                gfx.DrawString(col == 0 ? "Datos del cliente" : "Datos del vehículo", font, XBrushes.Black, new XRect(startX + col * cellWidth, startY, cellWidth, cellHeight), XStringFormats.Center);
            }

            // Filas de datos fijos
            string[] datosCliente = { $"Nombre: {resultado.Nombre}", $"Apellido: {resultado.Apellido}", $"Email: {resultado.EMail}" };
            string[] datosVehiculo = { $"Marca: {resultado.Marca}", $"Modelo: {resultado.Modelo}", $"Patente: {resultado.Patente}" };


            for (int r = 0; r < 3; r++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, startX, startY + (r + 1) * cellHeight, cellWidth, cellHeight);
                gfx.DrawString(datosCliente[r], font, XBrushes.Black, new XRect((startX + 5), startY + (r + 1) * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);

                gfx.DrawRectangle(XBrushes.LightGray, startX + cellWidth, startY + (r + 1) * cellHeight, cellWidth, cellHeight);
                gfx.DrawString(datosVehiculo[r], font, XBrushes.Black, new XRect((startX + 5) + cellWidth, startY + (r + 1) * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
            }


            startY += cellHeight * 6;

            gfx.DrawString("Desperfecto", font, XBrushes.Black, new XRect(startX, startY, cellWidth, cellHeight), XStringFormats.CenterLeft);
            gfx.DrawString("Valor", font, XBrushes.Black, new XRect(startX + cellWidth, startY, cellWidth, cellHeight), XStringFormats.CenterRight);

            int row = 1;

            foreach(Desperfecto desperfecto in resultado.Desperfectos)
            {
                gfx.DrawString(desperfecto.Descripcion, font, XBrushes.Black, new XRect(startX, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
                gfx.DrawString(" ", font, XBrushes.Gray, new XRect(startX + 10, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);

                row++;

                gfx.DrawString("Mano de obra", font, XBrushes.Gray, new XRect(startX + 10, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
                gfx.DrawString("$" + desperfecto.ManoDeObra.ToString("N2"), font, XBrushes.Black, new XRect(startX + cellWidth, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterRight);

                row++;

                gfx.DrawString("Día/s de estacionamiento $130 x "+ desperfecto.Tiempo, font, XBrushes.Gray, new XRect(startX + 10, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
                gfx.DrawString("$" + (desperfecto.Tiempo*130).ToString("N2"), font, XBrushes.Black, new XRect(startX + cellWidth, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterRight);

                row++;
                foreach (Repuesto repuesto in desperfecto.Repuestos)
                {
                    gfx.DrawString(repuesto.Nombre, font, XBrushes.Gray, new XRect(startX + 10, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
                    gfx.DrawString("$" + repuesto.Precio.ToString(), font, XBrushes.Black, new XRect(startX + cellWidth, startY + row * cellHeight, cellWidth, cellHeight), XStringFormats.CenterRight);
                    row++;
                }

            }

            startY += row * cellHeight;
            gfx.DrawString("Cargo 10% por trabajo realizado", font, XBrushes.Black, new XRect(startX, startY + cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
            gfx.DrawString("$" + ((int)resultado.Total * 0.10).ToString("N2"), font, XBrushes.Black, new XRect(startX + cellWidth, startY + cellHeight, cellWidth, cellHeight), XStringFormats.CenterRight);

            startY += cellHeight;
            gfx.DrawString("TOTAL", font, XBrushes.Black, new XRect(startX, startY + cellHeight, cellWidth, cellHeight), XStringFormats.CenterLeft);
            gfx.DrawString("$" + ((int)resultado.Total * 1.10m).ToString("N2"), font, XBrushes.Black, new XRect(startX + cellWidth, startY + cellHeight, cellWidth, cellHeight), XStringFormats.CenterRight);

            var pdfStream = new MemoryStream();
            document.Save(pdfStream, false);
            pdfStream.Position = 0;


            return pdfStream;
        }

        /// <summary>
        /// Lista de presupueusto. En caso de lista de ids vacía -> lista todos los presupuestos
        /// </summary>
        /// <param name="vehiculosIds"></param>
        /// <returns></returns>
        public static async Task<List<Presupuesto>> ListarPresupuestos(List<int> vehiculosIds)
        {
            List<Presupuesto> resultado = new List<Presupuesto>();

            await Task.Run(() =>
            {
                resultado = PresupuestoDB.spPresupuestosListar(vehiculosIds);
            });

            return resultado;
        }

        /// <summary>
        /// Calcula promedio de totales por marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static async Task<PreTotalMarcaModelo> ObtenerPromedioTotalPorMarca(string marca)
        {
            List<PreTotalMarcaModelo> totales = new List<PreTotalMarcaModelo>();

            await Task.Run(() => totales = PresupuestoDB.spPresupuestoListarPorMarcaModelo(marca));

            decimal totalPre = 0;
            decimal factor = 1.10m;  // 10% de ganancia del taller

            foreach (PreTotalMarcaModelo tot in totales)
            {
                totalPre += (tot.Total * factor);
            }

            PreTotalMarcaModelo resultado = null;

            if (totales != null && totales.Count() > 0)
            {
                resultado = new PreTotalMarcaModelo();
                resultado.Marca = marca;
                resultado.Modelo = "N/A";
                resultado.Total = (totalPre / totales.Count());
            }

            return resultado;
        }

        /// <summary>
        /// Calcula promedio de totales por modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public static async Task<PreTotalMarcaModelo> ObtenerPromedioTotalPorModelo(string modelo)
        {
            List<PreTotalMarcaModelo> totales = new List<PreTotalMarcaModelo>();

            await Task.Run(() => totales = PresupuestoDB.spPresupuestoListarPorMarcaModelo(null, modelo));

            decimal totalPre = 0;
            decimal factor = 1.10m; // 10% de ganancia del taller

            foreach (PreTotalMarcaModelo tot in totales)
            {
                totalPre += (tot.Total * factor);
            }

            PreTotalMarcaModelo resultado = null;

            if (totales != null && totales.Count() > 0)
            {
                resultado = new PreTotalMarcaModelo()
                {
                    Marca = totales[0].Marca,
                    Modelo = modelo,
                    Total = (totalPre / totales.Count())
                };

            }


            return resultado;
        }

        /// <summary>
        /// Total de Presupuestos para Autos y para Motos
        /// </summary>
        /// <returns></returns>
        public static async Task<Dictionary<string, decimal>> ObtenerTotalesAutosMotos()
        {
            Dictionary<string, decimal> resultado = new Dictionary<string, decimal>();

            await Task.Run(() => { resultado = PresupuestoDB.spPresupuestoTotalesAutosMotos(); });

            return resultado;
        }

        #endregion

        #region ESCRITURA
        /// <summary>
        ///  Carga un trabajo nuevo
        /// </summary>
        /// <param name="trabajoAutomovil"></param>
        /// <param name="trabajoMoto"></param>
        /// <returns></returns>
        public static async Task<bool> CargarTrabajoVehiculo(TrabajoAutomovilNuevo trabajoAutomovil = null, TrabajoMotoNuevo trabajoMoto = null) 
        {

            int presupuestoId = 0;

            if(trabajoAutomovil != null)
            {
                await Task.Run(() =>
                {
                    presupuestoId = PresupuestoDB.spVehiculoPresupuestoGuardar(trabajoAutomovil);
                });

                if(trabajoAutomovil.Presupuesto.Desperfectos != null && trabajoAutomovil.Presupuesto.Desperfectos.Count() > 0)
                {
                    foreach(Desperfecto desperfecto in trabajoAutomovil.Presupuesto.Desperfectos)
                    {
                        DesperfectosDB.GuardarDesperfectoRepuestos(desperfecto,presupuestoId);
                    }
                }
            }
            else if(trabajoMoto != null)
            {
                await Task.Run(() =>
                {
                    presupuestoId = PresupuestoDB.spVehiculoPresupuestoGuardar(null, trabajoMoto);
                });

                if (trabajoMoto.Presupuesto.Desperfectos != null && trabajoMoto.Presupuesto.Desperfectos.Count() > 0)
                {
                    foreach (Desperfecto desperfecto in trabajoMoto.Presupuesto.Desperfectos)
                    {
                        DesperfectosDB.GuardarDesperfectoRepuestos(desperfecto, presupuestoId);
                    }
                }
            }

            return true; 
        }
        #endregion
    }
}
