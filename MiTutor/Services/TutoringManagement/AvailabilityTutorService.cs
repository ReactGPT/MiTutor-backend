using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.TutoringManagement
{
    public class AvailabilityTutorService
    {
        private readonly DatabaseManager _databaseManager;

        public AvailabilityTutorService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task<List<ListAvailabilityTutor>> ListarDisponibilidadPorTutor(int tutorId)
        {
            List<ListAvailabilityTutor> disponibilidades = new List<ListAvailabilityTutor>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                        Value = tutorId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_DISPONIBILIDADES_X_TUTOR, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListAvailabilityTutor disponibilidad = new ListAvailabilityTutor
                        {
                            AvailabilityTutorId = Convert.ToInt32(row["AvailabilityTutorId"]),
                            AvailabilityDate = row["AvailabilityDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)row["AvailabilityDate"]) : default(DateOnly),
                            StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromTimeSpan((TimeSpan)row["StartTime"]) : default(TimeOnly),
                            EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromTimeSpan((TimeSpan)row["EndTime"]) : default(TimeOnly),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };

                        disponibilidades.Add(disponibilidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar la disponibilidad por tutor: " + ex.Message);
            }

            return disponibilidades;
        }

        public async Task InsertarDisponibilidad(CreateAvailabilityTutor availabilityTutor)
        {
            try
            {
                var availabilityDate = DateTime.Parse(availabilityTutor.AvailabilityDate);
                var startTime = TimeSpan.Parse(availabilityTutor.StartTime);
                var endTime = TimeSpan.Parse(availabilityTutor.EndTime);

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@AvailabilityDate", SqlDbType.Date) { Value = availabilityDate },
                    new SqlParameter("@StartTime", SqlDbType.Time) { Value = startTime },
                    new SqlParameter("@EndTime", SqlDbType.Time) { Value = endTime },
                    new SqlParameter("@IsActive", SqlDbType.Bit) { Value = 1 },
                    new SqlParameter("@TutorId", SqlDbType.Int) { Value = availabilityTutor.TutorId },
                    new SqlParameter("@AvailabilityTutorId", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_DISPONIBILIDAD, parameters);

                int availabilityTutorId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

                availabilityTutor.AvailabilityTutorId = availabilityTutorId;

                if (availabilityTutorId == -1)
                {
                    throw new Exception("Ya existe una disponibilidad en el rango horario especificado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la disponibilidad: " + ex.Message);
            }
        }

        public async Task<bool> EliminarDisponibilidad(int availabilityTutorId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@AvailabilityTutorId", SqlDbType.Int) { Value = availabilityTutorId }
                };

                int rowsAffected = await _databaseManager.ExecuteStoredProcedureWithRowsAffected(StoredProcedure.ELIMINAR_DISPONIBILIDAD, parameters);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la disponibilidad: " + ex.Message);
            }
        }


    }
}
