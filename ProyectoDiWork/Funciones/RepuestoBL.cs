using ProyectoDiWork.DataBase;
using ProyectoDiWork.Modelos;

namespace ProyectoDiWork.Funciones
{
    /// <summary>
    /// class RepuestoBL
    /// </summary>
    public class RepuestoBL
    {
#pragma warning disable CS1998
        /// <summary>
        /// Metodo para generar lista de repuestas desde la ejecución del sp Massive Charge
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Repuesto>> ExecSpMassiveCharge()
        {
            List<Repuesto> resultado = new List<Repuesto>();

            resultado = RepuestoDB.spMassiveCharge();

            return resultado;
        }
#pragma warning restore CS1998
    }
}
