using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.TutoringManagement
{
    public class TutorStudentProgramService
    {
        private readonly DatabaseManager _databaseManager;

        public TutorStudentProgramService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearTutorStudentProgram(TutorStudentProgramModificado tutorStudentProgramModificado)
        {
            await GetStudentProgramIdAsync(tutorStudentProgramModificado);
            SqlParameter[] parameters = new SqlParameter[]
            {
                 
                new SqlParameter("@StudentProgramId", SqlDbType.Int) { Value = tutorStudentProgramModificado.TutorStudentProgram.StudentProgramId },
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = tutorStudentProgramModificado.TutorStudentProgram.TutorId },
                new SqlParameter("@Motivo", SqlDbType.NVarChar) { Value = tutorStudentProgramModificado.TutorStudentProgram.Motivo}

            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_TUTOR_STUDENT_PROGRAM, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la relación Tutor-Student-Program: " + ex.Message);
            }
        }

        public async Task GetStudentProgramIdAsync(TutorStudentProgramModificado tutorStudentProgramModificado)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@studentId", SqlDbType.Int) { Value = tutorStudentProgramModificado.StudentId },
            new SqlParameter("@ProgramId", SqlDbType.Int) { Value = tutorStudentProgramModificado.ProgramId },
            new SqlParameter("@SelectedStudentProgramId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure("TUTOR_STUDENT_PROGRAM_Conseguir_FIND", parameters);
                // Obtener el ID del StudentProgram generado
                tutorStudentProgramModificado.TutorStudentProgram.StudentProgramId = Convert.ToInt32(parameters[parameters.Length - 1].Value);



            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el StudentProgramId: " + ex.Message);
            }
        }
    }
         
}
