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
 
    }
}
