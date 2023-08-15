using ProyectoDiWork.Funciones;
using ProyectoDiWork.Modelos;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ProyectoDiWork.DataBase
{
    /// <summary>
    /// class RepuestoDB
    /// </summary>
    public class RepuestoDB
    {
        #region LECTURA

        /// <summary>
        /// Ejecuta stored procedure spRepuestosLitar
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Repuesto> spRepuestosLitar()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spRepuestosListar";

                DataSet ds = DataBase.EjecutarConsulta(comando);

                List<Repuesto> resultado = new List<Repuesto>(); 
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Repuesto data = new Repuesto();
                        data.Id = Convert.ToInt32(dr["Id"]);
                        data.Nombre = dr["Nombre"].ToString();
                        data.Precio = Convert.ToDecimal(dr["Precio"]);
                        resultado.Add(data);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en sp spRepuestoMasUtilizadoPorModelo: " + ex.Message);
            }
        }

        /// <summary>
        /// sp para retornar repuesto mas utilizado por modelo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static RepuestoPorModelo spRepuestoMasUtilizadoPorModelo(string modelo)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spRepuestoMasUtilizadoPorModelo";

                comando.Parameters.AddWithValue("@Modelo", modelo);

                DataSet ds = DataBase.EjecutarConsulta(comando);

                RepuestoPorModelo resultado = null;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    resultado = new RepuestoPorModelo();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        resultado.Modelo = dr["Modelo"].ToString();
                        resultado.IdRepuesto = Convert.ToInt32(dr["Id"]);
                        resultado.Nombre = dr["Nombre"].ToString();
                        resultado.Cantidad = Convert.ToInt32(dr["cantidad"]);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en sp spRepuestoMasUtilizadoPorModelo: " + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta stored procedure spRepuestoMasUtilizadoPorModeloListar
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<RepuestoPorModelo> spRepuestoMasUtilizadoPorModeloListar()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spRepuestoMasUtilizadoPorModeloListar";

                DataSet ds = DataBase.EjecutarConsulta(comando);

                List<RepuestoPorModelo> resultado = new List<RepuestoPorModelo>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        RepuestoPorModelo data = new RepuestoPorModelo();
                        data.Modelo = dr["Modelo"].ToString();
                        data.IdRepuesto = Convert.ToInt32(dr["Id"]);
                        data.Nombre = dr["Nombre"].ToString();
                        data.Cantidad = Convert.ToInt32(dr["cantidad"]);

                        resultado.Add(data);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en sp spRepuestoMasUtilizadoPorModeloListar: " + ex.Message);
            }
        }

        /// <summary>
        /// sp para retornar repuesto mas utilizado por marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static RepuestoPorMarca spRepuestoMasUtilizadoPorMarca(string marca)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spRepuestoMasUtilizadoPorMarca";

                comando.Parameters.AddWithValue("@Marca", marca);

                DataSet ds = DataBase.EjecutarConsulta(comando);

                RepuestoPorMarca resultado = null;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    resultado = new RepuestoPorMarca();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        resultado.Marca = dr["Marca"].ToString();
                        resultado.IdRepuesto = Convert.ToInt32(dr["Id"]);
                        resultado.Nombre = dr["Nombre"].ToString();
                        resultado.Cantidad = Convert.ToInt32(dr["cantidad"]);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en sp spRepuestoMasUtilizadoPorMarca: " + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta stored procedure spRepuestoMasUtilizadoPorMarcaListar
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<RepuestoPorMarca> spRepuestoMasUtilizadoPorMarcaListar()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spRepuestoMasUtilizadoPorMarcaListar";

                DataSet ds = DataBase.EjecutarConsulta(comando);

                List<RepuestoPorMarca> resultado = new List<RepuestoPorMarca>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        RepuestoPorMarca data = new RepuestoPorMarca();
                        data.Marca = dr["Marca"].ToString();
                        data.IdRepuesto = Convert.ToInt32(dr["Id"]);
                        data.Nombre = dr["Nombre"].ToString();
                        data.Cantidad = Convert.ToInt32(dr["cantidad"]);
                        
                        resultado.Add(data);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en sp spRepuestoMasUtilizadoPorModeloListar: " + ex.Message);
            }
        }

        #endregion
        #region ESCRITURA
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
        #endregion
    }
}
