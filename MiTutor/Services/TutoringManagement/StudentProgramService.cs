using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.UniversityUnitManagement;
using MiTutor.Models;

namespace MiTutor.Services.TutoringManagement
{
    public class StudentProgramService
    {
        private readonly DatabaseManager _databaseManager;

        public StudentProgramService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearProgramaEstudiante(StudentProgram studentProgram)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentProgram.Student.Id},
                new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = studentProgram.TutoringProgram.TutoringProgramId },
                new SqlParameter("@JoinDate", SqlDbType.Date) { Value = studentProgram.JoinDate } 
            };
             
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_ESTUDIANTE_PROGRAMA, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear: " + ex.Message);
            }
        }
 
        public async Task<StudentProgram> ObtenerStudentProgramPorId(int studentProgramId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StudentProgramId", SqlDbType.Int) { Value = studentProgramId }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("OBTENER_STUDENT_PROGRAM_POR_ID", parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    StudentProgram studentProgram = new StudentProgram
                    {
                        StudentProgramId = Convert.ToInt32(row["StudentProgramId"]),
                        // Asigna los demás campos según la estructura de tu tabla
                    };

                    return studentProgram;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el StudentProgram: " + ex.Message);
            }
        }

        public async Task<List<Notificacion>> ListarNotificacionesPorUserAcount(int UserAcountId)
        {

            List<Notificacion> notificaciones = new List<Notificacion>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserAcountId", SqlDbType.Int) { Value = UserAcountId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_NOTIFICACIONES_POR_USUARIO, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Notificacion notificacion = new Notificacion
                        {
                            resumen = row[0].ToString(),
                            descripcion = row[1].ToString(),
                            tipo = row[2].ToString()

                        };
                        notificaciones.Add(notificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarNotificacionesPorUserAcount", ex);
            }


            return notificaciones;
        }

    }
}
