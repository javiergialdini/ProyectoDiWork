using System.Data;
using System.Data.SqlClient;

namespace DataBase.DataBase
{
    /// <summary>
    /// class DataBase
    /// </summary>
    public class DataBase
    {
        private static string ConexionSQL = System.Environment.GetEnvironmentVariable("ConexionSQL", EnvironmentVariableTarget.Process);

        /// <summary>
        /// Ejecuta una consulta y devuelve el resultado en un DataSet.
        /// </summary>
        /// <param name="comando">Comando a ejecutar</param>
        /// <returns></returns>
        public static DataSet EjecutarConsulta(SqlCommand comando)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionSQL))
                {
                    comando.Connection = conexion;
                    comando.CommandTimeout = 300;

                    using (SqlDataAdapter da = new SqlDataAdapter(comando))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión con la base de datos: " + ex.Message);
            }
        }
    }
}
