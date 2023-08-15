using ProyectoDiWork.DataBase;
using ProyectoDiWork.Modelos;

namespace ProyectoDiWork.Funciones
{
    /// <summary>
    /// class RepuestoBL
    /// </summary>
    public class RepuestoBL
    {

        #region LECTURA

        /// <summary>
        /// Obtiene el repuesto mas utilizado para el modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public static async Task<RepuestoPorModelo> ObtenerMayorPorModelo(string modelo)
        {
            RepuestoPorModelo resultado = new RepuestoPorModelo();

            await Task.Run(() => { resultado = RepuestoDB.spRepuestoMasUtilizadoPorModelo(modelo);});

            return resultado;
        }

        /// <summary>
        /// Lista el repuesto mas utilizado por modelo
        /// </summary>
        /// <returns></returns>
        public static async Task<List<RepuestoPorModelo>> ListarMayorPorModelo()
        {
            List<RepuestoPorModelo> resultado = new List<RepuestoPorModelo>();

            await Task.Run(() => { resultado = RepuestoDB.spRepuestoMasUtilizadoPorModeloListar(); });

            return resultado;
        }

        /// <summary>
        /// Obtiene el repuesto mas utilizado para el marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static async Task<RepuestoPorMarca> ObtenerMayorPorMarca(string marca)
        {
            RepuestoPorMarca resultado = new RepuestoPorMarca();

            await Task.Run(() => { resultado = RepuestoDB.spRepuestoMasUtilizadoPorMarca(marca); });

            return resultado;
        }

        /// <summary>
        /// Lista el repuesto mas utilizado por marca
        /// </summary>
        /// <returns></returns>
        public static async Task<List<RepuestoPorMarca>> ListarMayorPorMarca()
        {
            List<RepuestoPorMarca> resultado = new List<RepuestoPorMarca>();

            await Task.Run(() => { resultado = RepuestoDB.spRepuestoMasUtilizadoPorMarcaListar(); });

            return resultado;
        }

        #endregion

        #region ESCRITURA
        /// <summary>
        /// Metodo para generar lista de repuestas desde la ejecución del sp Massive Charge
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Repuesto>> ExecSpMassiveCharge()
        {
            List<Repuesto> resultado = new List<Repuesto>();

            await Task.Run(() => { resultado = RepuestoDB.spMassiveCharge(); });

            return resultado;
        }
        #endregion
    }
}
