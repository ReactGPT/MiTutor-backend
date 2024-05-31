using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Services.TutoringManagement
{
    public class StudentProgramService
    {
        private readonly DatabaseManager _databaseManager;

        public StudentProgramService()
        {
            _databaseManager = new DatabaseManager();
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
    }
}
