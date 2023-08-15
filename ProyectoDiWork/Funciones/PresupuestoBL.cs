﻿using ProyectoDiWork.DataBase;
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
