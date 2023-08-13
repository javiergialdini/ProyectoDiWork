using ProyectoDiWork.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoDiWork.DataBase
{
    /// <summary>
    /// class RepuestoDB
    /// </summary>
    public class RepuestoDB
    {
        /// <summary>
        /// Ejecuta sp MassiveCharge
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Repuesto> spMassiveCharge()
        {
            try
            {
                List<Repuesto> respuesta = new List<Repuesto>();

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "MassiveCharge";

                DataSet ds = DataBase.EjecutarConsulta(comando);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Repuesto res = new Repuesto() 
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"].ToString())
                        };
                        respuesta.Add(res);
                    }
                }

                return respuesta;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el sp MassiveCharge: " + ex.Message);
            }
        }
    }
}
