﻿using ProyectoDiWork.Funciones;
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
        #region LECTURA
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