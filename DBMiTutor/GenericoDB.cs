using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;


namespace DBMiTutor
{
    public class GenericoDB
    {
        private static string GetConnectionString()
        {
            string path = "config.json";
            string json = File.ReadAllText(path);
            dynamic config = JsonConvert.DeserializeObject(json);

            if (config != null)
            {
                string dataSource = config.DataSource ?? string.Empty;
                string initialCatalog = config.InitialCatalog ?? string.Empty;
                string userID = config.UserID ?? string.Empty;
                string password = config.Password ?? string.Empty;

                return $"data source={dataSource};initial catalog={initialCatalog};user id={userID};password={password}";
            }
            else
            {
                throw new Exception("Error: Config object is null.");
            }
        }
        
        public static SqlCommand CreateCommand(string storeName)
        {
            string conexionSQL = @"data source = reactgpt-db.cfi59sgrpoq2.us-east-1.rds.amazonaws.com;"
                                + "initial catalog =mitutor; user id = admin; password = ErAnYelHZCWt55igAZZg";

            SqlConnection conn = new(conexionSQL);
            SqlCommand cmd = new(storeName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;

        }

        public static DataTable ExecuteStore(SqlCommand cmd)
        {
            cmd.Connection.Open();
            SqlDataAdapter da = new(cmd);
            DataTable dataTable = new();

            da.Fill(dataTable);
            cmd.Connection.Close();
            da.Dispose();
            return dataTable;
        }



        public static int ExecuteStoreNonquery(SqlCommand cmd)
        {
            cmd.Connection.Open();
            int rows = cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            return rows;
        }

    }
}