using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Services.UserManagement;
using MiTutor.Models.GestionUsuarios;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace MiTutor.Services.TutoringManagement
{
    public class AppointmentResultService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly CommentService _commentService;

        public AppointmentResultService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
            _commentService = new CommentService(_databaseManager);
        }

        public async Task AgregarResultadoCita(int studentId, int tutoringProgramId, int id_appointment)
        {
            int id_result = 0;
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@id_student", SqlDbType.Int) { Value =  studentId},
                new SqlParameter("@id_program", SqlDbType.Int) { Value =  tutoringProgramId},
                new SqlParameter("@id_appointment", SqlDbType.Int) { Value =  id_appointment},
                new SqlParameter("@asistio", SqlDbType.Bit) { Value = 0 },
                new SqlParameter("@appointment_result_id", SqlDbType.Int) { Direction = ParameterDirection.Output }

            };

            try
            {

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_RESULTADO_CITA, parameters);

                // Obtener el ID del resultado recién insertado
                id_result = Convert.ToInt32(parameters[parameters.Length - 1].Value);
                Comment comentVacio = new Comment();
                comentVacio.PrivacyTypeId = 1;
                comentVacio.AppointmentResultId= id_result;
                await _commentService.CrearComentario(comentVacio);
                comentVacio.PrivacyTypeId = 2;
                await _commentService.CrearComentario(comentVacio);


            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el resultado de cita");
            }
        }

        public async Task<List<int>> AgregarResultadoCitaOriginal(InsertAppointmentResult appointmentResult, DateTime startTime, DateTime endTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id_appointment", SqlDbType.Int) { Value =  appointmentResult.appointmentId},
                new SqlParameter("@id_student", SqlDbType.Int) { Value =  appointmentResult.studentId},
                new SqlParameter("@id_program", SqlDbType.Int) { Value =  appointmentResult.tutoringProgramId},
                new SqlParameter("@asistio", SqlDbType.Bit) { Value = appointmentResult.appointmentResult.Asistio },
                new SqlParameter("@start_time", SqlDbType.DateTime) { Value = startTime},
                new SqlParameter("@end_time", SqlDbType.DateTime) { Value = endTime },
                new SqlParameter("@appointment_result_id", SqlDbType.Int) { Direction = ParameterDirection.Output }
            }; 

            try
            {

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_RESULTADO_CITA, parameters);
                appointmentResult.appointmentResult.AppointmentResultId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

                // Llamar al método para realizar otra operación con el ID de la cita
                foreach (Comment comment in appointmentResult.appointmentResult.Comments)
                {
                    // Aquí llamas al servicio de comentarios para crear cada comentario
                    comment.AppointmentResultId = appointmentResult.appointmentResult.AppointmentResultId;
                    await _commentService.CrearComentario(comment);
                }
                return [Convert.ToInt32(appointmentResult.appointmentResult.AppointmentResultId), 
                    appointmentResult.appointmentResult.Comments[0].CommentId,
                    appointmentResult.appointmentResult.Comments[1].CommentId];
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el resultado de cita");
            }

        }
        public async Task<InsertAppointmentResult> ConsultarResultadoCita(int appointmentId, int studentId, int tutoringProgramId)
        {
            InsertAppointmentResult appointmentResult = null; // Inicializa como null

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@id_appointment", SqlDbType.Int) { Value = appointmentId },
        new SqlParameter("@id_student", SqlDbType.Int) { Value = studentId },
        new SqlParameter("@id_program", SqlDbType.Int) { Value = tutoringProgramId }
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.CONSULTAR_RESULTADO_CITA, parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    appointmentResult = new InsertAppointmentResult
                    {
                        appointmentId = appointmentId,
                        studentId = studentId,
                        tutoringProgramId = tutoringProgramId,
                        appointmentResult = new AppointmentResult()
                    };

                    foreach (DataRow row in dataTable.Rows)
                    {
                        appointmentResult.appointmentResult.AppointmentResultId = Convert.ToInt32(row[0]);
                        appointmentResult.appointmentResult.IsActive = Convert.ToBoolean(row[1]);
                        appointmentResult.appointmentResult.Asistio = Convert.ToBoolean(row[3]);
                        appointmentResult.appointmentResult.StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromTimeSpan(((DateTime)row["StartTime"]).TimeOfDay) : default;
                        appointmentResult.appointmentResult.EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromTimeSpan(((DateTime)row["EndTime"]).TimeOfDay) : default;
                    }

                    await _commentService.ConsultarComentario_x_ID_ResultadoCita(appointmentResult.appointmentResult.AppointmentResultId, appointmentResult.appointmentResult.Comments);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el resultado de cita", ex);
            }

            return appointmentResult;
        }


        //ActualizarResultadoCita
        public async Task ActualizarResultadoCita(int id_appointmentResult, bool asistio, DateTime startTime, DateTime endTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@asistio", SqlDbType.Bit) { Value = asistio },
                new SqlParameter("@id_appointment_result", SqlDbType.Int) { Value = id_appointmentResult },
                new SqlParameter("@start_time", SqlDbType.DateTime) { Value = startTime },
                new SqlParameter("@end_time", SqlDbType.DateTime) { Value = endTime }
            };
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_RESULTADO_CITA, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarResultadoCita");
            }
        }

    }
}
