using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models;

namespace MiTutor.Services.GestionUsuarios
{
    public class StudentService
    {
        private readonly DatabaseManager _databaseManager;

        public StudentService()
        {
            _databaseManager = new DatabaseManager();
        }
        public async Task CrearEstudiante(Student student)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", SqlDbType.Bit) { Value = student.Id },
                new SqlParameter("@IsRisk", SqlDbType.Bit) { Value = student.IsRisk },
                new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = student.SpecialityId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_ESTUDIANTE, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearEstudiante");
            }
        }

        public async Task<List<Student>> ListarEstudiantes()
        {
            List<Student> students = new List<Student>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESTUDIANTES, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Student student = new Student();
                        student.Usuario = new UserAccount();

                        student.Id = Convert.ToInt32(row["PersonId"]);
                        student.Name = row["Name"].ToString();
                        student.LastName = row["LastName"].ToString();
                        student.SecondLastName = row["SecondLastName"].ToString();
                        student.Phone = row["Phone"].ToString();
                        student.IsRisk = Convert.ToBoolean(row["IsRisk"]);
                        student.SpecialityId = Convert.ToInt32(row["SpecialtyId"]);
                        student.IsActive = Convert.ToBoolean(row["IsActive"]); 
                        student.Usuario.InstitutionalEmail = row["InstitutionalEmail"].ToString();
                        student.Usuario.PUCPCode = row["PUCPCode"].ToString();

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantes", ex);
            }

            return students;
        }

    }
}
