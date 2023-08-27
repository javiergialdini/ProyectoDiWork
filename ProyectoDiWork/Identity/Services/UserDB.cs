using Newtonsoft.Json;
using ProyectoDiWork.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoDiWork.Identity.Services
{
    /// <summary>
    /// UserDB
    /// </summary>
    public class UserDB
    {
        /// <summary>
        /// Si existe usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static CustomUserStore GetUser(CustomUserStore user)
        {
            try
            {
                CustomUserStore respuesta = new CustomUserStore();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = $"Select u.IdUsuario, u.UserName, u.Password from Usuario u where u.UserName = '{user.UserName}' and u.Password = '{user.Password}'";

                DataSet ds = DataBase.DataBase.EjecutarConsulta(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        respuesta.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        respuesta.UserName = dr["UserName"].ToString();
                        respuesta.Password = dr["Password"].ToString();
                    }
                    return respuesta;
                }
                else 
                {
                    return null; 
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Error al comunicarse con la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshTokenRequest"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static HistorialRefreshToken GetRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
        {
            try
            {
                HistorialRefreshToken respuesta = new HistorialRefreshToken();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = $"SELECT IdHistorialToken, IdUsuario, Token, RefreshToken, FechaCreacion, FechaExpiracion, EsActivo FROM HistorialRefreshToken WHERE Token = '{refreshTokenRequest.TokenExpirado}' AND RefreshToken = '{refreshTokenRequest.RefreshToken}' AND IdUsuario = {idUsuario}";

                DataSet ds = DataBase.DataBase.EjecutarConsulta(command);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        respuesta.IdHistorialToken = Convert.ToInt32(dr["IdHistorialToken"]);
                        respuesta.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                        respuesta.Token = dr["Token"].ToString();
                        respuesta.RefreshToken = dr["RefreshToken"].ToString();
                        respuesta.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                        respuesta.FechaExpiracion = Convert.ToDateTime(dr["FechaExpiracion"]);
                        respuesta.EsActivo = Convert.ToBoolean(dr["EsActivo"]);

                    }
                    return respuesta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al comunicarse con la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// SaveHistorialRefreshToken
        /// </summary>
        /// <param name="historialRefreshToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool SaveHistorialRefreshToken(HistorialRefreshToken historialRefreshToken)
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spGuardarHistorialRefreshToken";

                comando.Parameters.AddWithValue("@IdUsuario", historialRefreshToken.IdUsuario);
                comando.Parameters.AddWithValue("@Token", historialRefreshToken.Token);
                comando.Parameters.AddWithValue("@RefreshToken", historialRefreshToken.RefreshToken);
                comando.Parameters.AddWithValue("@FechaCreacion", historialRefreshToken.FechaCreacion);
                comando.Parameters.AddWithValue("@FechaExpiracion", historialRefreshToken.FechaExpiracion);

                DataSet ds = DataBase.DataBase.EjecutarConsulta(comando);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comunicarse con la base de datos: " + ex.Message);
            }
        }
    }
}
