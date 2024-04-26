using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMiTutor
{
    public class MiTutorDB
    {

        public int ESP_InsertarEspecilidad(string nombre, string username)
        {
            SqlCommand cmd = GenericoDB.CreateCommand("ESP_CrearEspecialidad_Insert");

            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar));
            

            cmd.Parameters["@nombre"].Value = nombre;
            cmd.Parameters["@username"].Value = username;
            

            int rowsAffected = GenericoDB.ExecuteStoreNonquery(cmd);
            return rowsAffected;
        }
        public DataTable ESP_ListarEspecialidades()
        {
            SqlCommand cmd = GenericoDB.CreateCommand("ESP_ListarEspecialidades_Select");
            DataTable dt = GenericoDB.ExecuteStore(cmd);
            return dt;
        }
    }
}
