using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MiTutor.DataAccess
{
    public class DatabaseManager
    {
        private static string _connectionString = @"data source = react.cw9aj8lxrqii.us-east-1.rds.amazonaws.com;"
                                + "initial catalog =mitutor; user id = admin; password = irwEguLpecJujhmCnKDg";

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

        public async Task<int> ExecuteStoredProcedureWithRowsAffected(string storedProcedureName, SqlParameter[] parameters)
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
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error al ejecutar el Stored Procedure: {storedProcedureName}", ex);
                    }
                }
            }
        }

        public async Task<int> MeasureDatabaseResponseTimeAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT 1", connection))
                {
                    try
                    {
                        var stopwatch = Stopwatch.StartNew();
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        stopwatch.Stop();
                        return (int)stopwatch.ElapsedMilliseconds;
                    }
                    catch
                    {
                        return -1; // Indica un error
                    }
                }
            }
        }


    }
}
