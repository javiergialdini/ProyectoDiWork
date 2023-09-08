using Modelos.Modelos;
using System.Data.SqlClient;
using System.Data;

namespace DataBase.DataBase
{
    /// <summary>
    /// DesperfectosBL
    /// </summary>
    public class DesperfectosDB
    {
        /// <summary>
        /// Ejecuta sp para guardar desperfecto con los repuestos de un presupuesto
        /// </summary>
        /// <param name="desperfecto"></param>
        /// <param name="presupuestoId"></param>
        /// <exception cref="Exception"></exception>
        public static void GuardarDesperfectoRepuestos(Desperfecto desperfecto, int presupuestoId)
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "spDesperfectoRepuestosGuardar";

                comando.Parameters.AddWithValue("@idPresupuesto", presupuestoId);
                comando.Parameters.AddWithValue("@Descripcion", desperfecto.Descripcion);
                comando.Parameters.AddWithValue("@ManoDeObra", desperfecto.ManoDeObra);
                comando.Parameters.AddWithValue("@Tiempo", desperfecto.Tiempo);

                if(desperfecto.Repuestos != null && desperfecto.Repuestos.Count() > 0)
                {
                    DataTable dtRepuestos = new DataTable();
                    dtRepuestos.Columns.Add("id", typeof(int));
                    dtRepuestos.Columns.Add("Nombre", typeof(string));
                    dtRepuestos.Columns.Add("Precio", typeof(decimal));

                    foreach (Repuesto repuesto in desperfecto.Repuestos)
                    {
                        DataRow r = dtRepuestos.NewRow();
                        r["id"] = repuesto.Id;
                        r["Nombre"] = repuesto.Nombre;
                        r["Precio"] = repuesto.Precio;

                        dtRepuestos.Rows.Add(r);
                    }
                    comando.Parameters.AddWithValue("@Repuestos", dtRepuestos);
                }

                DataSet ds = DataBase.EjecutarConsulta(comando);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el sp spDesperfectoRepuestosGuardar: " + ex.Message);
            }
        }
    }
}
