using ProyectoDiWork.Modelos;
using System.Data;
using System.Data.SqlClient;
using static ProyectoDiWork.Modelos.Enumerados;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ProyectoDiWork.DataBase
{
    /// <summary>
    /// PresupuestoDB
    /// </summary>
    public class PresupuestoDB
    {
        #region LECTURA

        /// <summary>
        /// Ejecuta spObtenerPresupuesto
        /// </summary>
        /// <param name="presupuestoId"></param>
        /// <param name="idVehiculo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Presupuesto spPresupuestoObtener(int? presupuestoId = null, int? idVehiculo = null)
        {
            try
            {
                Presupuesto resultado = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spPresupuestoObtener";
                if(presupuestoId != null && presupuestoId > 0)
                    comando.Parameters.AddWithValue("@presupuestoId", presupuestoId);
                if (idVehiculo != null && idVehiculo > 0)
                    comando.Parameters.AddWithValue("@idVehiculo", idVehiculo);


                DataSet ds = DataBase.EjecutarConsulta(comando);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    resultado = new Presupuesto();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        resultado.Id = Convert.ToInt32(dr["presupuestoId"]);
                        resultado.Nombre = dr["Nombre"].ToString();
                        resultado.Apellido = dr["Apellido"].ToString();
                        resultado.EMail = dr["EMail"].ToString();
                        resultado.idVehiulo = Convert.ToInt32(dr["idVehiculo"]);

                        if(dr["Desperfectos"] != DBNull.Value)
                            resultado.Desperfectos = JsonConvert.DeserializeObject<List<Desperfecto>>(dr["Desperfectos"].ToString());
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el sp spPresupuestoObtener: " + ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta spObtenerPresupuesto
        /// </summary>
        /// <param name="vehiculosIds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<Presupuesto> spPresupuestosListar(List<int> vehiculosIds)
        {
            try
            {
                List<Presupuesto> resultado = new List<Presupuesto>();

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spPresupuestosListar";

                DataTable dtIds = new DataTable();
                dtIds.Columns.Add("id", typeof(int));

                foreach (int id in vehiculosIds)
                {
                    DataRow r = dtIds.NewRow();
                    r["id"] = id;
                    dtIds.Rows.Add(r);
                }
                comando.Parameters.AddWithValue("@vehiculosIds", dtIds);


                DataSet ds = DataBase.EjecutarConsulta(comando);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Presupuesto presupuesto = new Presupuesto();
                        presupuesto.Id = Convert.ToInt32(dr["presupuestoId"]);
                        presupuesto.Nombre = dr["Nombre"].ToString();
                        presupuesto.Apellido = dr["Apellido"].ToString();
                        presupuesto.EMail = dr["EMail"].ToString();
                        presupuesto.idVehiulo = Convert.ToInt32(dr["idVehiculo"]);

                        if (dr["Desperfectos"] != DBNull.Value)
                            presupuesto.Desperfectos = JsonConvert.DeserializeObject<List<Desperfecto>>(dr["Desperfectos"].ToString());

                        resultado.Add(presupuesto);
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el sp spPresupuestoObtener: " + ex.Message);
            }
        }


        #endregion
        #region ESCRITURA
        /// <summary>
        /// Guarda Automovil y presupuesto
        /// </summary>
        /// <param name="trabajoAutomovil"></param>
        /// <param name="trabajoMoto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int spVehiculoPresupuestoGuardar(TrabajoAutomovilNuevo trabajoAutomovil = null, TrabajoMotoNuevo trabajoMoto = null)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spVehiculoPresupuestoGuardar";

                comando.Parameters.Add("@idPresupuesto", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                if (trabajoAutomovil != null)
                {
                    comando.Parameters.AddWithValue("@Marca", trabajoAutomovil.Automovil.Marca);
                    comando.Parameters.AddWithValue("@Modelo", trabajoAutomovil.Automovil.Modelo);
                    comando.Parameters.AddWithValue("@Patente", trabajoAutomovil.Automovil.Patente);
                    comando.Parameters.AddWithValue("@TipoAutomovil", trabajoAutomovil.Automovil.Tipo);
                    comando.Parameters.AddWithValue("@CantidadPuertasAuto", trabajoAutomovil.Automovil.CantidadPuertas);
                    comando.Parameters.AddWithValue("@Nombre", trabajoAutomovil.Presupuesto.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", trabajoAutomovil.Presupuesto.Apellido);
                    comando.Parameters.AddWithValue("@Email", trabajoAutomovil.Presupuesto.EMail);
                }
                else if(trabajoMoto != null)
                {
                    comando.Parameters.AddWithValue("@Marca", trabajoMoto.Moto.Marca);
                    comando.Parameters.AddWithValue("@Modelo", trabajoMoto.Moto.Modelo);
                    comando.Parameters.AddWithValue("@Patente", trabajoMoto.Moto.Patente);
                    comando.Parameters.AddWithValue("@Cilindrada", trabajoMoto.Moto.Cilindrada);
                    comando.Parameters.AddWithValue("@Nombre", trabajoMoto.Presupuesto.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", trabajoMoto.Presupuesto.Apellido);
                    comando.Parameters.AddWithValue("@Email", trabajoMoto.Presupuesto.EMail);
                }

                DataSet ds = DataBase.EjecutarConsulta(comando);

                int presupuestoId = (int)comando.Parameters["@idPresupuesto"].Value;

                return presupuestoId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el sp spVehiculoPresupuestoGuardar: " + ex.Message);
            }
        }
        #endregion
    }
}
