using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models;
using MiTutor.Models.TutoringManagement;
using MiTutor.Services.UserManagement;
using System.Linq.Expressions;

namespace MiTutor.Services.GestionUsuarios
{
    public class StudentService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly UserAccountService _userAccountService;

        public StudentService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
            _userAccountService = new UserAccountService(_databaseManager);
        }

        public async Task CrearEstudiante(StudentTodo student)
        {
            UserAccount _userAccount = new UserAccount(); //crear usuario y persona
            _userAccount.Id = student.PersonId;                                                        
            _userAccount.InstitutionalEmail =student.InstitutionalEmail;
            _userAccount.PUCPCode = student.PUCPCode;

            _userAccount.Persona = new Person();
            _userAccount.Persona.Name = student.Name;
            _userAccount.Persona.LastName = student.LastName;
            _userAccount.Persona.SecondLastName = student.SecondLastName;
            _userAccount.Persona.Phone = student.Phone;

            await _userAccountService.CrearUsuario(_userAccount);
            if(_userAccount.Id != -1)
            {
                SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = _userAccount.Id },
                new SqlParameter("@IsRisk", SqlDbType.Bit) { Value = 0 },
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

        public async Task<List<StudentTodo>> ListarEstudiantesTodo()
        {
            List<StudentTodo> students = new List<StudentTodo>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TODO_ESTUDIANTE, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentTodo student = new StudentTodo();

                        student.PersonId = Convert.ToInt32(row["PersonId"]);
                        student.Name = row["Name"].ToString();
                        student.LastName = row["LastName"].ToString();
                        student.SecondLastName = row["SecondLastName"].ToString();
                        student.Phone = row["Phone"].ToString();
                        student.PersonIsActive = Convert.ToBoolean(row["PersonIsActive"]);

                        student.IsRisk = Convert.ToBoolean(row["IsRisk"]);

                        student.SpecialityId = Convert.ToInt32(row["SpecialtyId"]);
                        student.SpecialtyName = Convert.ToString(row["SpecialtyName"]);
                        student.SpecialtyAcronym = Convert.ToString(row["SpecialtyAcronym"]);

                        student.FacultyId = Convert.ToInt32(row["FacultyId"]);
                        student.FacultyName = Convert.ToString(row["FacultyName"]);
                        student.FacultyAcronym = Convert.ToString(row["FacultyAcronym"]);
                        
                        
                        student.UserIsActive = Convert.ToBoolean(row["UserIsActive"]);
                        student.InstitutionalEmail = row["InstitutionalEmail"].ToString();
                        student.PUCPCode = row["PUCPCode"].ToString();
                        student.CreationDate = Convert.ToDateTime(row["CreationDate"]).Date;
                        student.ModificationDate = Convert.ToDateTime(row["ModificationDate"]).Date;

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantesTodoCompleto", ex);
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
                throw new Exception("ERROR en ListarAlumnosConCantidadDeProgramas", ex);
            }
            return students;
        }
        public async Task<List<StudentContadorProgramasAcademicos>> ListarAlumnosConCantidadDeProgramas()
        {
            List<StudentContadorProgramasAcademicos> alumnos = new List<StudentContadorProgramasAcademicos>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CONTADOR_PROGRAMAS_POR_ALUMNO);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentContadorProgramasAcademicos alumno = new StudentContadorProgramasAcademicos
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            StudentName = row["StudentName"].ToString(),
                            StudentLastName = row["LastName"].ToString(),
                            StudentSecondLastName = row["SecondLastName"].ToString(),
                            CantidadProgramas = Convert.ToInt32(row["ProgramCount"])
                        };

                        alumnos.Add(alumno);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarAlumnosConCantidadDeProgramas", ex);
            }

            return alumnos;
        }

        public async Task<List<ListarCantidadAppointmentsStudent>> ListarCantidadAppointmentsStudent()
        {
            List<ListarCantidadAppointmentsStudent> appointments = new List<ListarCantidadAppointmentsStudent>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CANTIDAD_CITAS_ALUMNO);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarCantidadAppointmentsStudent appointment = new ListarCantidadAppointmentsStudent
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            StudentName = row["StudentName"].ToString(),
                            StudentLastName = row["LastName"].ToString(),
                            StudentSecondLastName = row["SecondLastName"].ToString(),
                            TotalAppointments = Convert.ToInt32(row["TotalAppointments"]),
                            RegisteredCount = Convert.ToInt32(row["RegisteredCount"]),
                            PendingResultCount = Convert.ToInt32(row["PendingResultCount"]),
                            CompletedCount = Convert.ToInt32(row["CompletedCount"])
                        };

                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarCantidadAppointmentsStudent", ex);
            }

            return appointments;
        }

        public async Task<List<StudentTutoringProgram>> ListarProgramaFechaStudent(int studentId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<StudentTutoringProgram> programs = new List<StudentTutoringProgram>();

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@StudentId", studentId),
                    new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
                    new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ALUMNO_FECHA_PROGRAMA, parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentTutoringProgram program = new StudentTutoringProgram
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            StudentCount = Convert.ToInt32(row["StudentCount"])
                        };

                        programs.Add(program);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en GetTutorPrograms", ex);
            }

            return programs;
        }     

        public async Task<List<StudentAppointment>> ListarAppointmentPorFechaStudent(int studentId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<StudentAppointment> appointments = new List<StudentAppointment>();

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@StudentId", studentId),
                    new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
                    new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ALUMNO_FECHA_CITA, parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentAppointment appointment = new StudentAppointment
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            StudentName = row["StudentName"].ToString(),
                            StudentLastName = row["LastName"].ToString(),
                            StudentSecondLastName = row["SecondLastName"].ToString(),
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            StartTime = Convert.ToDateTime(row["StartTime"]),
                            EndTime = Convert.ToDateTime(row["EndTime"]),
                            CreationDate = DateOnly.FromDateTime(Convert.ToDateTime(row["CreationDate"])),

                            Reason = row["Reason"].ToString(),
                            AppointmentTutorId = Convert.ToInt32(row["AppointmentTutorId"]),
                            AppointmentStatusId = Convert.ToInt32(row["AppointmentStatusId"]),
                            Classroom = string.IsNullOrEmpty(row["Classroom"].ToString()) ? "NoHayLink" : row["Classroom"].ToString(),

                            IsInPerson = Convert.ToInt32(row["IsInPerson"]),
                            AppointmentStatusName = row["AppointmentStatusName"].ToString(),
                            FacultyName = row["FacultyName"].ToString(),
                            StudentCount = Convert.ToInt32(row["StudentCount"])
                        };

                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEstudiantesPorIdProgramaTutoria", ex);
            }

            return appointments;
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
                throw new Exception("ERROR en ListarAppointmentPorFecha" + ex.Message, ex);
            }                
                        
            

            return students;
        }     

        public async Task<List<StudentProgramVirtualFace>> ListarProgramaVirtualFaceStudent(int studentId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<StudentProgramVirtualFace> programasVirtualFace = new List<StudentProgramVirtualFace>();

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@StudentId", studentId),
                    new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
                    new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("STUDENT_PROGRAM_VIRTUAL_FACE_SELECT", parameters.ToArray());

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentProgramVirtualFace programaVirtualFace = new StudentProgramVirtualFace
                        {
                            CantidadPresenciales = Convert.ToInt32(row["CantidadPresenciales"]),
                            CantidadVirtuales = Convert.ToInt32(row["CantidadVirtuales"])
                        };

                        programasVirtualFace.Add(programaVirtualFace);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarProgramaVirtualFace" + ex.Message, ex);
            }

            return programasVirtualFace;
        }      

        public async Task<List<ListarStudentJSON2>> ListarEstudiantesPorIdCita(int appointmentId)
        {
            List<ListarStudentJSON2> students = new List<ListarStudentJSON2>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AppointId", SqlDbType.Int){
                        Value = appointmentId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ALUMNOS_POR_ID_CITA, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarStudentJSON2 student = new ListarStudentJSON2()
                        {
                            StudentId = Convert.ToInt32(row["PersonId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            PUCPCode = row["PUCPCode"].ToString(),
                            IsRisk = Convert.ToBoolean(row["IsRisk"]),
                            Asistio = row["Asistio"] != DBNull.Value ? Convert.ToBoolean(row["Asistio"]) : false
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
    }
}
