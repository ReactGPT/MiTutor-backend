using System.Data;
using System.Data.SqlClient;

namespace MiTutor.DataAccess
{
    public class DatabaseManager
    {
        private static string _connectionString = @"data source = reactgpt-db.cfi59sgrpoq2.us-east-1.rds.amazonaws.com;"
                                + "initial catalog =mitutor; user id = admin; password = ErAnYelHZCWt55igAZZg";

        public async Task ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch
                    {
                        throw new Exception("Error al ejecutar el Stored Procedure: " + storedProcedureName);
                    }
                }
            }
        }

        public async Task<DataTable> ExecuteStoredProcedureDataTable(string storedProcedureName, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        await connection.OpenAsync();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                    catch
                    {
                        throw new Exception("Error al ejecutar el Stored Procedure: " + storedProcedureName);
                    }
                }
            }
            return dataTable;
        }
    }
}
