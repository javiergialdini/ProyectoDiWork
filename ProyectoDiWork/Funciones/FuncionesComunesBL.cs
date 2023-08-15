using ProyectoDiWork.Modelos;

namespace ProyectoDiWork.Funciones
{
    /// <summary>
    /// Clase para almacenar funciones comunes
    /// </summary>
    public class FuncionesComunesBL
    {
        /// <summary>
        /// Chequea si se cargaron datos para Automovil o moto
        /// </summary>
        /// <param name="auto"></param>
        /// <param name="moto"></param>
        /// <returns></returns>
        public static bool CheckVehiDatos(Automovil auto = null, Moto moto = null)
        {
            if(auto != null && auto.Tipo > 0
               && auto.Marca != null && auto.Marca != ""
               && auto.Modelo != null && auto.Modelo != ""
               && auto.CantidadPuertas > 0) 
            {
                return true;
            }
            if (moto != null && moto.Marca != null && moto.Marca != ""
               && moto.Modelo != null && moto.Modelo != ""
               && moto.Cilindrada > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chequea si se cargaron datos del cliente
        /// </summary>
        /// <param name="pre"></param>
        /// <returns></returns>
        public static bool CheckClienteDatos(Presupuesto pre)
        {
            if(pre.Nombre != null && pre.Nombre != "" 
                && pre.Apellido != null && pre.Apellido != "" 
                && pre.EMail != null && pre.EMail != "")
            {
                return true;
            }
            return false;
        }

    }
}
