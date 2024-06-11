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

        public StudentService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
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

        public async Task<List<StudentTutoria>> ListarEstudiantesPorIdProgramaTutoria(int programaTutoriaId)
        {
            List<StudentTutoria> students = new List<StudentTutoria>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int){
                        Value = programaTutoriaId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESTUDIANTES_POR_PROGRAMA_TUTORIA, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentTutoria student = new StudentTutoria();
                        student.Usuario = new UserAccount();
                        student.Person = new Person();

                        student.Id = Convert.ToInt32(row["StudentId"]);
                        student.Person.Name = row["Name"].ToString();
                        student.Person.LastName = row["LastName"].ToString();
                        student.Person.SecondLastName = row["SecondLastName"].ToString();
                        student.IsActive = Convert.ToBoolean(row["IsActive"]);
                        student.Usuario.PUCPCode = row["PUCPCode"].ToString();
                        student.Usuario.InstitutionalEmail = row["InstitutionalEmail"].ToString();
                        student.FacultyName = row["FacultyName"].ToString();
                        if (!row.IsNull("TutorId"))
                        {
                            student.IdTutor = Convert.ToInt32(row["TutorId"]);
                            student.TutorName = row["TutorName"].ToString() + " " + row["TutorLastName"].ToString() + " " + row["TutorSecondLastName"].ToString();
                        }
                        else
                        {
                            student.IdTutor = 0;
                            student.TutorName = "";
                        }
                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantesPorIdProgramaTutoria", ex);
            }

            return students;
        }

        public async Task<List<StudentIdVerified>> ListarEstudiantesPorId(List<StudentIdVerified> studentsVerified)
        {
            List<StudentIdVerified> students = new List<StudentIdVerified>();

            try
            {
                foreach (StudentIdVerified s in studentsVerified)
                {
                    SqlParameter[] parameters = new SqlParameter[]{
                        new SqlParameter("@StudentId", SqlDbType.Int){
                            Value = s.pucpCode
                        }
                    };
                    DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESTUDIANTES_POR_ID, parameters);
                    if (dataTable != null)
                    {
                        StudentIdVerified student = new StudentIdVerified();
                        if (dataTable.Rows.Count != 0)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                student.studentId = Convert.ToInt32(row["PersonId"]);
                                student.name = row["Name"].ToString();
                                student.lastName = row["LastName"].ToString();
                                student.secondLastName = row["SecondLastName"].ToString();
                                student.isActive = Convert.ToBoolean(row["IsActive"]);
                                student.pucpCode = row["PUCPCode"].ToString();
                                student.institutionalEmail = row["InstitutionalEmail"].ToString();
                                student.facultyName = row["FacultyName"].ToString();
                                students.Add(student);
                            }
                        }
                        else
                        {
                            student.studentId = 0;
                            student.name = s.name;
                            student.lastName = s.lastName;
                            student.secondLastName = s.secondLastName;
                            student.isActive = s.isActive;
                            student.pucpCode = s.pucpCode;
                            student.institutionalEmail = s.institutionalEmail;
                            student.facultyName = s.facultyName;
                            students.Add(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantesPorId", ex);
            }
            return students;
        }
    }
}
