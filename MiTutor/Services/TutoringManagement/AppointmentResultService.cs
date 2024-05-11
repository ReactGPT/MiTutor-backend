using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Services.UserManagement;
using MiTutor.Models.GestionUsuarios;
using System.Xml.Linq;

namespace MiTutor.Services.TutoringManagement
{
    public class AppointmentResultService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly CommentService _commentService;

        public AppointmentResultService()
        {
            _databaseManager = new DatabaseManager();
            _commentService = new CommentService();
        }

        public async Task AgregarResultadoCita(InsertAppointmentResult appointmentResult)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {  
                new SqlParameter("@id_appointment", SqlDbType.Int) { Value =  appointmentResult.appointmentId},
                new SqlParameter("@id_student", SqlDbType.Int) { Value =  appointmentResult.studentId},
                new SqlParameter("@id_program", SqlDbType.Int) { Value =  appointmentResult.tutoringProgramId},
                new SqlParameter("@asistio", SqlDbType.Bit) { Value = appointmentResult.appointmentResult.Asistio },
                new SqlParameter("@appointment_result_id", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_RESULTADO_CITA, parameters);

                // Obtener el ID del resultado recién insertado
                appointmentResult.appointmentResult.AppointmentResultId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

                // Llamar al método para realizar otra operación con el ID de la cita
                foreach (Comment comment in appointmentResult.appointmentResult.Comments)
                {
                    // Aquí llamas al servicio de comentarios para crear cada comentario
                    comment.AppointmentResultId = appointmentResult.appointmentResult.AppointmentResultId;
                    await _commentService.CrearComentario(comment);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el resultado de cita");
            }
        }

        public async Task<InsertAppointmentResult> ConsultarResultadoCita(int appointmentId,int studentId,int tutoringProgramId)
        {
            InsertAppointmentResult appointmentResult= new InsertAppointmentResult();
            appointmentResult.appointmentId = appointmentId;
            appointmentResult.studentId = studentId;
            appointmentResult.tutoringProgramId = tutoringProgramId;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id_appointment", SqlDbType.Int) { Value =  appointmentResult.appointmentId},
                new SqlParameter("@id_student", SqlDbType.Int) { Value =  appointmentResult.studentId},
                new SqlParameter("@id_program", SqlDbType.Int) { Value =  appointmentResult.tutoringProgramId} 
            };
 
            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.CONSULTAR_RESULTADO_CITA, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        appointmentResult.appointmentResult.AppointmentResultId = Convert.ToInt32(row[0]);
                        appointmentResult.appointmentResult.IsActive = Convert.ToBoolean(row[1]); 
                        appointmentResult.appointmentResult.Asistio = Convert.ToBoolean(row[3]);
                    } 
                }
                 
                await _commentService.ConsultarComentario_x_ID_ResultadoCita(appointmentResult.appointmentResult.AppointmentResultId, appointmentResult.appointmentResult.Comments); 
                 

            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el resultado de cita");
            }

            return appointmentResult;
        }
        //ActualizarResultadoCita
        public async Task ActualizarResultadoCita(int id_appointmentResult,bool asistio)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@asistio", SqlDbType.Bit) { Value = asistio},
                new SqlParameter("@id_appointment_result", SqlDbType.Int) { Value = id_appointmentResult},
                
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
