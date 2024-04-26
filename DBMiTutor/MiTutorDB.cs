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

        public int ESP_ActualizarEspecialidad(int id, string nombre)
        {
            SqlCommand cmd = GenericoDB.CreateCommand("ESP_ActualizarEspecialidad_Update");

            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));

            cmd.Parameters["@id"].Value = id;
            cmd.Parameters["@nombre"].Value = nombre;

            int rowsAffected = GenericoDB.ExecuteStoreNonquery(cmd);
            return rowsAffected;
        }

        public int ESP_EliminarEspecialidad(int id)
        {
            SqlCommand cmd = GenericoDB.CreateCommand("ESP_EliminarEspecialidad_Delete");
            
            
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            var wasEliminated = new SqlParameter("@Eliminado", SqlDbType.Int);
            wasEliminated.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(wasEliminated);
            
            cmd.Parameters["@id"].Value = id;

            int rowsAffected = GenericoDB.ExecuteStoreNonquery(cmd);
            return int.Parse(wasEliminated.Value.ToString());
        }
    }
}
