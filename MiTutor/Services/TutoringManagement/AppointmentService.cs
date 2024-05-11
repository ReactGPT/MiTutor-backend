using MiTutor.DataAccess;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.TutoringManagement;

namespace MiTutor.Services.TutoringManagement
{
    public class AppointmentService
    {
        private readonly DatabaseManager _databaseManager;

        public AppointmentService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task AgregarCita(RegisterAppointment registerAppointment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
               // new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = registerAppointment.appointment.StartTime },
               // new SqlParameter("@EndTime", SqlDbType.DateTime) { Value = registerAppointment.appointment.EndTime},
               // new SqlParameter("@CreationDate", SqlDbType.Date) { Value = registerAppointment.appointment.CreationDate },
                new SqlParameter("@Reason", SqlDbType.NVarChar, 255) { Value = registerAppointment.appointment.Reason},
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = registerAppointment.IdTutor },
                new SqlParameter("@AppointmentStatusId", SqlDbType.Int) { Value = 1 },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = true },
                new SqlParameter("@IsInPerson", SqlDbType.Bit) { Value = registerAppointment.appointment.IsInPerson },
                new SqlParameter("@Classroom", SqlDbType.NVarChar, 255) { Value = registerAppointment.appointment.Classroom },
                new SqlParameter("@AppointmentId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.AGREGAR_CITA, parameters);

                // Obtener el ID de la cita recién insertada
                registerAppointment.appointment.AppointmentId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

                // Llamar al método para realizar otra operación con el ID de la cita
                await RealizarOtraOperacionConCitaId(registerAppointment);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cita: " + registerAppointment.appointment.AppointmentId + ex.Message);
            }
        }
        /*{
  "appointment": {
    "appointmentId": 0,
    "reason": "Razón de la cita",
    "isActive": true,
    "isInPerson": true,
    "classroom": "Salón 101",
    "appointmentStatus": {
      "appointmentStatusId": 0,
      "name": "string",
      "isActive": true
    }
  },
  "idProgramTutoring": 4,
  "idTutor": 2,
  "idStudent": [
    2
  ]
}*/
        private async Task RealizarOtraOperacionConCitaId(RegisterAppointment registerAppointment)
        {


            // Llamar al procedimiento almacenado para obtener el ID del StudentProgram
            foreach (int studentId in registerAppointment.IdStudent)
            {
                // Crear parámetros para llamar al procedimiento almacenado
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId },
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = registerAppointment.IdProgramTutoring},
                    new SqlParameter("@StudentProgramId", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                try
                {
                    // Ejecutar el procedimiento almacenado
                    await _databaseManager.ExecuteStoredProcedure("GetStudentProgramId", parameters);
                    // Obtener el ID del StudentProgram generado
                    int studentProgramId = Convert.ToInt32(parameters[parameters.Length - 1].Value);


                    // Crear nuevos parámetros para el procedimiento InsertarAppointmentStudentProgram
                    SqlParameter[] insertParameters = new SqlParameter[]
                    {
                        new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = registerAppointment.appointment.AppointmentId },
                        new SqlParameter("@StudentProgramId", SqlDbType.Int) { Value = studentProgramId }
                    };

                    // Ejecutar el procedimiento almacenado para insertar en la tabla AppointmentStudentProgram
                    await _databaseManager.ExecuteStoredProcedure("InsertarAppointmentStudentProgram", insertParameters);


                    // Aquí puedes realizar cualquier operación adicional con el ID del StudentProgram
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el ID del StudentProgram: " + ex.Message);
                }
            }
        }
    }
}
