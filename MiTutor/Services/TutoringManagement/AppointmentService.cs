using MiTutor.DataAccess;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.TutoringManagement;
using MiTutor.Models.GestionUsuarios;

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
               // new SqlParameter("@StartTime", SqlDbType.DateTime) { Value = registerAppointment.Appointment.StartTime },
               // new SqlParameter("@EndTime", SqlDbType.DateTime) { Value = registerAppointment.Appointment.EndTime},
               // new SqlParameter("@CreationDate", SqlDbType.Date) { Value = registerAppointment.Appointment.CreationDate },
                new SqlParameter("@Reason", SqlDbType.NVarChar, 255) { Value = registerAppointment.Appointment.Reason},
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = registerAppointment.IdTutor },
                new SqlParameter("@AppointmentStatusId", SqlDbType.Int) { Value = 1 },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = true },
                new SqlParameter("@IsInPerson", SqlDbType.Bit) { Value = registerAppointment.Appointment.IsInPerson },
                new SqlParameter("@Classroom", SqlDbType.NVarChar, 255) { Value = registerAppointment.Appointment.Classroom },
                new SqlParameter("@AppointmentId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.AGREGAR_CITA, parameters);

                // Obtener el ID de la cita recién insertada
                registerAppointment.Appointment.AppointmentId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

                // Llamar al método para realizar otra operación con el ID de la cita
                await RealizarOtraOperacionConCitaId(registerAppointment);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cita: " + registerAppointment.Appointment.AppointmentId + ex.Message);
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
                        new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = registerAppointment.Appointment.AppointmentId },
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

        public async Task<List<ListarAppointment>> ListarCitasPorTutor(int tutorId)
        {
            List<ListarAppointment> citas = new List<ListarAppointment>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                        Value = tutorId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITA_POR_TUTOR, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarAppointment cita = new ListarAppointment
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            ProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            AppointmentStatus = row["AppointmentStatus"].ToString(),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            CreationDate = row["CreationDate"] != DBNull.Value ? DateOnly.FromDateTime(((DateTime)row["CreationDate"])) : default,
                            PersonId = Convert.ToInt32(row["PersonId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            IsInPerson = Convert.ToBoolean(row["IsInPerson"]),
                            StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromTimeSpan(((DateTime)row["StartTime"]).TimeOfDay) : default,
                            EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromTimeSpan(((DateTime)row["EndTime"]).TimeOfDay) : default,
                            Reason = row["Reason"].ToString()
                        };

                        citas.Add(cita);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las citas por tutor: " + ex.Message);
            }

            return citas;
        }

        public async Task<List<ListarAppointment>> ListarCitasPorTutorPorAlumno(int tutorId, int studentId)
        {
            List<ListarAppointment> citas = new List<ListarAppointment>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                        Value = tutorId
                    },
                    new SqlParameter("@StudentId", SqlDbType.Int){
                        Value = studentId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITA_POR_TUTOR_POR_ALUMNO, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarAppointment cita = new ListarAppointment
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            AppointmentStatus = row["AppointmentStatus"].ToString(),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            CreationDate = row["CreationDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)row["CreationDate"]) : default(DateOnly),
                            PersonId = Convert.ToInt32(row["PersonId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            IsInPerson = Convert.ToBoolean(row["IsInPerson"]),
                            StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["StartTime"]) : default(TimeOnly),
                            EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["EndTime"]) : default(TimeOnly),
                            Reason = row["Reason"].ToString()
                        };

                        citas.Add(cita);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las citas por tutor y alumno: " + ex.Message);
            }

            return citas;
        }


        public async Task<List<ListarAppointment>> ListarCitasPorAlumno(int studentId)
        {
            List<ListarAppointment> citas = new List<ListarAppointment>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StudentId", SqlDbType.Int){
                        Value = studentId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITA_POR_ALUMNO, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarAppointment cita = new ListarAppointment
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            AppointmentStatus = row["AppointmentStatus"].ToString(),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            CreationDate = row["CreationDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)row["CreationDate"]) : default(DateOnly),
                            PersonId = Convert.ToInt32(row["PersonId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            IsInPerson = Convert.ToBoolean(row["IsInPerson"]),
                            StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["StartTime"]) : default(TimeOnly),
                            EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["EndTime"]) : default(TimeOnly),
                            Reason = row["Reason"].ToString()
                        };

                        citas.Add(cita);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las citas por alumno: " + ex.Message);
            }

            return citas;
        }

        public async Task<List<ListarAppointment>> ListarCitasPorID(int appointId)
        {
            List<ListarAppointment> citas = new List<ListarAppointment>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AppointId", SqlDbType.Int){
                        Value = appointId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITA_POR_ID, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarAppointment cita = new ListarAppointment
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            AppointmentStatus = row["AppointmentStatus"].ToString(),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            CreationDate = row["CreationDate"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)row["CreationDate"]) : default(DateOnly),
                            PersonId = Convert.ToInt32(row["PersonId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            IsInPerson = Convert.ToBoolean(row["IsInPerson"]),
                            StartTime = row["StartTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["StartTime"]) : default(TimeOnly),
                            EndTime = row["EndTime"] != DBNull.Value ? TimeOnly.FromDateTime((DateTime)row["EndTime"]) : default(TimeOnly),
                            Reason = row["Reason"].ToString()
                        };

                        citas.Add(cita);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las citas por alumno: " + ex.Message);
            }

            return citas;
        }

    }
}

