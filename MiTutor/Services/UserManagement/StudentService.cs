using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models;
using MiTutor.Models.TutoringManagement;

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

        public async Task<List<ListarStudentJSON>> ListarEstudiantesByTutoringProgram(int tutoringProgramId)
        {
            List<ListarStudentJSON> students = new List<ListarStudentJSON>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int){
                        Value = tutoringProgramId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESTUDIANTES_POR_PROGRAMA, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarStudentJSON student = new ListarStudentJSON()
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            PUCPCode = row["PUCPCode"].ToString(),
                            InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                            Phone = row["Phone"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString(),
                            FacultyName = row["FacultyName"].ToString()
                        };

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantesByTutoringProgram", ex);
            }

            return students;
        }

        public async Task<ListarStudentJSON> SeleccionarDatosEstudiantesById(int studentId)
        {
            ListarStudentJSON student = null;
            
            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StudentId", SqlDbType.Int){
                         Value = studentId
                    }
            };

            DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.SELECCIONAR_ESTUDIANTE_X_ID, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0]; // Selecciona la primera fila ya que esperas solo un estudiante

                    student = new ListarStudentJSON()
                    {
                        Name = row["Name"].ToString(),
                        LastName = row["LastName"].ToString(),
                        SecondLastName = row["SecondLastName"].ToString(),
                        PUCPCode = row["PUCPCode"].ToString(),
                        InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                        Phone = row["Phone"].ToString(),
                        SpecialtyName = row["SpecialtyName"].ToString(),
                        FacultyName = row["FacultyName"].ToString()
                    };
                    student.StudentId = studentId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en SeleccionarDatosEstudiantesById", ex);
            }
            
            return student;
        }

    }
}
