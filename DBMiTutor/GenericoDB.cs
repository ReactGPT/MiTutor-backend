using System.Data;
using System.Data.SqlClient;


namespace DBMiTutor
{
    public class GenericoDB
    {
        public static SqlCommand CreateCommand(string storeName)
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.);
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