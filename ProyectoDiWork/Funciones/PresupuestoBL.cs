using ProyectoDiWork.DataBase;
using ProyectoDiWork.Modelos;

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

            return resultado;
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

            PreTotalMarcaModelo resultado = new PreTotalMarcaModelo() 
            {
                Marca = marca,
                Modelo = "N/A",
                Total = (totalPre / totales.Count())
            };

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

            PreTotalMarcaModelo resultado = new PreTotalMarcaModelo()
            {
                Marca = totales[0].Marca,
                Modelo = modelo,
                Total = (totalPre / totales.Count())
            };

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
