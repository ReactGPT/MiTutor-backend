using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.TutoringManagement;
using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Services.TutoringManagement
{
    public class TutorService
    {
        private readonly DatabaseManager _databaseManager;

        public TutorService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearTutor(Tutor tutor)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@MeetingRoom", SqlDbType.NVarChar) { Value = tutor.MeetingRoom },
                new SqlParameter("@UserAccountId", SqlDbType.Int) { Value = tutor.UserAccount.Id }
                };

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_TUTOR, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el tutor: " + ex.Message);
            }
        }
        public async Task CrearTutoresBatch(List<Tutor> tutores)
        {
            SqlParameter[] parameters;

            DataTable dtTutores = new DataTable();
            dtTutores.Columns.Add("IdTutor", typeof(int));
            dtTutores.Columns.Add("IdAccount", typeof(int));
            
            
            try
            {
                foreach (Tutor tutor in tutores)
                {
                    dtTutores.Rows.Add(tutor.TutorId, tutor.UserAccount.Id);
                }

                parameters = new SqlParameter[]
                {
                new SqlParameter("@TableTutores", SqlDbType.Structured) { Value = dtTutores }
                };

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREARBORRAR_TUTORES, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear los tutores: " + ex.Message);
            }
        }
        public async Task<List<Tutor>> ListarTutores(int IdProgramaTutoria)
        {
            List<Tutor> tutores = new List<Tutor>();
            
            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTORES, IdProgramaTutoria==-1?null:new SqlParameter[] {new SqlParameter("@idProgramaTutoria",SqlDbType.Int) {Value=IdProgramaTutoria } });

                if (dataTable != null && dataTable.Rows.Count>0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Tutor tutor = new Tutor
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            MeetingRoom = row["MeetingRoom"].ToString(),
                            UserAccount = new UserAccount {
                                Id = Convert.ToInt32(row["UserAccountId"]),
                                Persona = new Person
                                {
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["PersonLastName"].ToString(),
                                    SecondLastName = row["PersonSecondLastName"].ToString()
                                },
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                                PUCPCode = row["PUCPCode"].ToString()
                            },
                            ModificationDate = row["ModificationDate"].ToString()
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores: " + ex.Message);
            }

            return tutores;
        }
        public async Task<List<Tutor>> ListarTutoresPorNombreApellido(string nombreApellido)
        {
            List<Tutor> tutores = new List<Tutor>();

            try
            {
                DataTable dataTable;

                if (string.IsNullOrWhiteSpace(nombreApellido))
                {
                    dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_LISTAR_SELECT_TODOS");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@NombreApellido", SqlDbType.NVarChar) { Value = nombreApellido }
                    };

                    dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_LISTAR_SELECT_NOMBRE_APELLIDO", parameters);
                }

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Tutor tutor = new Tutor
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            MeetingRoom = row["MeetingRoom"].ToString(),
                            UserAccount = new UserAccount
                            {
                                Id = Convert.ToInt32(row["UserAccountId"]),
                                Persona = new Person
                                {
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["PersonLastName"].ToString(),
                                    SecondLastName = row["PersonSecondLastName"].ToString()
                                }
                            }
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores: " + ex.Message);
            }

            return tutores;
        }
        public async Task<List<Tutor>> ListarTutoresTipo()
        {
            List<Tutor> tutores = new List<Tutor>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTORES_TIPO, null);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Tutor tutor = new Tutor
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            MeetingRoom = row["MeetingRoom"] == DBNull.Value ? null : row["MeetingRoom"].ToString(),
                            IsActive = Convert.ToBoolean(row["TutorIsActive"]),
                            UserAccount = new UserAccount
                            {
                                Id = Convert.ToInt32(row["UserAccountId"]),
                                InstitutionalEmail = row["InstitutionalEmail"] == DBNull.Value ? null : row["InstitutionalEmail"].ToString(),
                                PUCPCode = row["PUCPCode"] == DBNull.Value ? null : row["PUCPCode"].ToString(),
                                IsActive = Convert.ToBoolean(row["UserAccountIsActive"]),
                                Persona = new Person
                                {
                                    Id = Convert.ToInt32(row["PersonId"]),
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["PersonLastName"].ToString(),
                                    SecondLastName = row["PersonSecondLastName"].ToString(),
                                    Phone = row["Phone"] == DBNull.Value ? null : row["Phone"].ToString(),
                                    IsActive = Convert.ToBoolean(row["PersonIsActive"])
                                }
                            },
                            Faculty = new Faculty
                            {
                                Name = row["FacultyName"].ToString()
                            },
                            TutoringProgram = new TutoringProgram
                            {
                                ProgramName = row["ProgramName"].ToString()
                            }
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores: " + ex.Message);
            }

            return tutores;
        }

        public async Task<Tutor> SeleccionarTutorxID(int tutorId)
        {
            Tutor tutor = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                         Value = tutorId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.SELECCIONAR_TUTOR_X_ID, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0]; // Selecciona la primera fila ya que esperas solo un estudiante

                    tutor = new Tutor()
                    {
                        UserAccount = new UserAccount
                        {
                            InstitutionalEmail = row["InstitutionalEmail"] == DBNull.Value ? null : row["InstitutionalEmail"].ToString(),

                            Persona = new Person
                            {
                                Name = row["PersonName"].ToString(),
                                LastName = row["PersonLastName"].ToString(),
                                SecondLastName = row["PersonSecondLastName"].ToString()
                            }
                        }
                    };
                    tutor.TutorId = tutorId;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("ERROR en SeleccionarTutor", ex);
            }

            return tutor;
        }

        public async Task<List<Tutor>> ListarTutoresPorPrograma(int idProgram)
        {
            List<Tutor> tutores = new List<Tutor>();

            try
            {
                // Crear los parámetros para el procedimiento almacenado
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@IdProgram", SqlDbType.Int) { Value = idProgram }
                };

                // Ejecutar el procedimiento almacenado y obtener el DataTable
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_PROGRAM_LISTAR_SELECT", parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Tutor tutor = new Tutor
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            MeetingRoom = row["MeetingRoom"] == DBNull.Value ? null : row["MeetingRoom"].ToString(),
                            IsActive = Convert.ToBoolean(row["TutorIsActive"]),
                            UserAccount = new UserAccount
                            {
                                Id = Convert.ToInt32(row["UserAccountId"]),
                                InstitutionalEmail = row["InstitutionalEmail"] == DBNull.Value ? null : row["InstitutionalEmail"].ToString(),
                                PUCPCode = row["PUCPCode"] == DBNull.Value ? null : row["PUCPCode"].ToString(),
                                IsActive = Convert.ToBoolean(row["UserAccountIsActive"]),
                                Persona = new Person
                                {
                                    Id = Convert.ToInt32(row["PersonId"]),
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["PersonLastName"].ToString(),
                                    SecondLastName = row["PersonSecondLastName"].ToString(),
                                    Phone = row["Phone"] == DBNull.Value ? null : row["Phone"].ToString(),
                                    IsActive = Convert.ToBoolean(row["PersonIsActive"])
                                }
                            },
                            Faculty = new Faculty
                            {
                                FacultyId = Convert.ToInt32(row["FacultyId"]),
                                Name = row["FacultyName"].ToString()
                            },
                            TutoringProgram = new TutoringProgram
                            {
                                TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                                ProgramName = row["ProgramName"].ToString()
                            }
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores por programa: " + ex.Message);
            }

            return tutores;
        }

        public async Task<List<TutorXtutoringProgramXalumno>> ListarTutoresPorTutoriaPorAlumno(int idProgram, int studentId)
        {
            List<TutorXtutoringProgramXalumno> tutores = new List<TutorXtutoringProgramXalumno>();

            try
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = idProgram },
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTORES_PROGRAM_ALUMNO, parameters);

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count != 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            TutorXtutoringProgramXalumno tutor = new TutorXtutoringProgramXalumno
                            {
                                TutorId = Convert.ToInt32(row["TutorId"]),
                                TutorName = row["TutorName"].ToString(),
                                TutorLastName = row["TutorLastName"].ToString(),
                                TutorSecondLastName = row["TutorSecondLastName"].ToString(),
                                State = row["State"].ToString()
                            };

                            tutores.Add(tutor);

                        }
                    }
                        
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores: " + ex.Message);
            }

            return tutores;
        }

        public async Task<List<TutorXtutoringProgramXalumno>> ListarTutoresPorTutoriaVariable(int idProgram)
        {
            List<TutorXtutoringProgramXalumno> tutores = new List<TutorXtutoringProgramXalumno>();

            try
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = idProgram }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTORES_PROGRAM_VARIABLE, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorXtutoringProgramXalumno tutor = new TutorXtutoringProgramXalumno
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["TutorLastName"].ToString(),
                            TutorSecondLastName = row["TutorSecondLastName"].ToString(),
                            State = "VARIABLE"
                        };

                            tutores.Add(tutor);
                        }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutores: " + ex.Message);
            }

            return tutores;
        }

        public async Task<Tutor> ObtenerTutorPorId(int tutorId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TutorId", SqlDbType.Int) { Value = tutorId }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("OBTENER_TUTOR_POR_ID", parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    Tutor tutor = new Tutor
                    {
                        TutorId = Convert.ToInt32(row["TutorId"]),
                        // Asigna los demás campos según la estructura de tu tabla
                    };

                    return tutor;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Tutor: " + ex.Message);
            }
        }

        public async Task<List<TutorContadorProgramasAcademicos>> ListarTutoresConCantidadDeProgramas()
        {
            List<TutorContadorProgramasAcademicos> tutores = new List<TutorContadorProgramasAcademicos>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CONTADOR_PROGRAMAS_POR_TUTOR);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorContadorProgramasAcademicos tutor = new TutorContadorProgramasAcademicos
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["LastName"].ToString(),
                            TutorSecondLastName = row["SecondLastName"].ToString(),
                            CantidadProgramas = Convert.ToInt32(row["ProgramCount"])
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTutoresConCantidadDeProgramas", ex);
            }

            return tutores;
        }
        public async Task<List<ListarCantidadAppointment>> ListarCantidadAppointments()
        {
            List<ListarCantidadAppointment> appointments = new List<ListarCantidadAppointment>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CANTIDAD_CITAS_TUTOR);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarCantidadAppointment appointment = new ListarCantidadAppointment
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["LastName"].ToString(),
                            TutorSecondLastName = row["SecondLastName"].ToString(),
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
                throw new Exception("ERROR en ListarCantidadAppointments", ex);
            }

            return appointments;
        }


        public async Task<List<TutorProgram>> ListarProgramaFecha(int tutorId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<TutorProgram> programs = new List<TutorProgram>();

            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@TutorId", tutorId),
            new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
            new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTOR_FECHA_PROGRAMA, parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorProgram program = new TutorProgram
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


        public async Task<List<TutorAppointment>> ListarAppointmentPorFecha(int tutorId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<TutorAppointment> appointments = new List<TutorAppointment>();

            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TutorId", tutorId),
                new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
                new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
            };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTOR_FECHA_CITA, parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorAppointment appointment = new TutorAppointment
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["LastName"].ToString(),
                            TutorSecondLastName = row["SecondLastName"].ToString(),
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
                throw new Exception("ERROR en ListarAppointmentPorFecha" + ex.Message, ex);
            }

            return appointments;
        }


        public async Task<List<TutorProgramVirtualFace>> ListarProgramaVirtualFace(int tutorId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<TutorProgramVirtualFace> programasVirtualFace = new List<TutorProgramVirtualFace>();

            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@TutorId", tutorId),
            new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
            new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_PROGRAM_VIRTUAL_FACE_SELECT", parameters.ToArray());

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorProgramVirtualFace programaVirtualFace = new TutorProgramVirtualFace
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

        public async Task<List<int>> ListarTutoresPorIdFacultad(int idFaculty)
        {
            List<int> tutorIds = new List<int>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FacultyId", SqlDbType.Int){
                Value = idFaculty
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTOR_ID_FACULTAD, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        int id = Convert.ToInt32(row["TutorId"]);
                        tutorIds.Add(id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTutoresPorIdFacultad", ex);
            }
            return tutorIds;
        }

        public async Task<List<int>> ListarTutoresPorIdEspecialidad(int idSpeciality)
        {
            List<int> tutorIds = new List<int>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@SpecialityId", SqlDbType.Int){
                Value = idSpeciality
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTOR_ID_ESPECIALIDAD, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        int id = Convert.ToInt32(row["TutorId"]);
                        tutorIds.Add(id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTutoresPorIdEspecialidad", ex);
            }
            return tutorIds;
        }

        //UPDATE

        public async Task<List<StudentInfo>> ListarAlumnosPorIdTutor(int tutorId)
        {
            List<StudentInfo> studentInfos = new List<StudentInfo>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@TutorId", SqlDbType.Int){
                Value = tutorId
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ALUMNOS_POR_TUTOR_UPDATE, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentInfo studentInfo = new StudentInfo
                        {
                            StudentId = Convert.ToInt32(row["StudentId"]),
                            Name = row["Name"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            Phone = row["Phone"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString(),
                            FacultyName = row["FacultyName"].ToString()
                        };
                        studentInfos.Add(studentInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarAlumnosPorIdTutor", ex);
            }
            return studentInfos;
        }


        public async Task<int> ContarEstudiantesPorIdTutor(int tutorId)
        {
            int studentCount = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@TutorId", SqlDbType.Int){
                Value = tutorId
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.ALUMNOS_CANTIDAD_POR_TUTOR, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    studentCount = Convert.ToInt32(dataTable.Rows[0]["StudentCount"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ContarEstudiantesPorIdTutor", ex);
            }
            return studentCount;
        }


        public async Task<List<TutoringProgramInfo>> ListarTodosProgramasDeTutoriaPorIdTutor(int tutorId)
        {
            List<TutoringProgramInfo> programInfos = new List<TutoringProgramInfo>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@TutorId", SqlDbType.Int){
                Value = tutorId
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMAS_TUTOR, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutoringProgramInfo programInfo = new TutoringProgramInfo
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            Description = row["Description"].ToString(),
                            FaceToFace = Convert.ToBoolean(row["FaceToFace"]),
                            Virtual = Convert.ToBoolean(row["Virtual"]),
                            FacultyName = row["FacultyName"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString()
                        };
                        programInfos.Add(programInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTodosProgramasDeTutoriaPorIdTutor", ex);
            }
            return programInfos;
        }


        public async Task<List<AppointmentInfo>> ListarTodasCitasPorIdTutor(int tutorId)
        {
            List<AppointmentInfo> appointmentInfos = new List<AppointmentInfo>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@TutorId", SqlDbType.Int){
                Value = tutorId
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITAS_TUTOR_UPDATE, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        AppointmentInfo appointmentInfo = new AppointmentInfo
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            StudentName = row["StudentName"].ToString(),
                            StudentLastName = row["StudentLastName"].ToString(),
                            StudentSecondLastName = row["StudentSecondLastName"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        appointmentInfos.Add(appointmentInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTodasCitasPorIdTutor", ex);
            }
            return appointmentInfos;
        }


        public async Task<List<StudentProgramInfo>> ListarProgramasPorEstudianteYTutor(int tutorId, int studentId)
        {
            List<StudentProgramInfo> programInfos = new List<StudentProgramInfo>();

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

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMAS_TUTOR_ESTUDIANTE, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentProgramInfo programInfo = new StudentProgramInfo
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            Description = row["Description"].ToString(),
                            FaceToFace = Convert.ToBoolean(row["FaceToFace"]),
                            Virtual = Convert.ToBoolean(row["Virtual"]),
                            FacultyName = row["FacultyName"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString()
                        };
                        programInfos.Add(programInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarProgramasPorEstudianteYTutor", ex);
            }
            return programInfos;
        }

        public async Task<List<StudentAppointmentInfo>> ListarCitasPorEstudianteYTutor(int tutorId, int studentId)
        {
            List<StudentAppointmentInfo> appointmentInfos = new List<StudentAppointmentInfo>();

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

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_CITAS_TUTOR_ESTUDIANTE, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentAppointmentInfo appointmentInfo = new StudentAppointmentInfo
                        {
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            StudentName = row["StudentName"].ToString(),
                            StudentLastName = row["StudentLastName"].ToString(),
                            StudentSecondLastName = row["StudentSecondLastName"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        appointmentInfos.Add(appointmentInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarCitasPorEstudianteYTutor", ex);
            }
            return appointmentInfos;
        }

        public async Task<int> ContarCitasPorEstudianteYTutor(int tutorId, int studentId)
        {
            int appointmentCount = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TutorId", SqlDbType.Int)
            {
                Value = tutorId
            },
            new SqlParameter("@StudentId", SqlDbType.Int)
            {
                Value = studentId
            }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.CONTAR_CITAS_TUTOR_ESTUDIANTE, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    appointmentCount = Convert.ToInt32(dataTable.Rows[0]["AppointmentCount"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ContarCitasPorEstudianteYTutor", ex);
            }
            return appointmentCount;
        }

        public async Task<StudentDetailedInfo> ObtenerInfoEstudiantePorTutor(int tutorId, int studentId)
        {
            StudentDetailedInfo studentInfo = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@TutorId", SqlDbType.Int)
            {
                Value = tutorId
            },
            new SqlParameter("@StudentId", SqlDbType.Int)
            {
                Value = studentId
            }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.OBTENER_INFO_TUTOR_ESTUDIANTE, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    studentInfo = new StudentDetailedInfo
                    {
                        StudentId = Convert.ToInt32(row["StudentId"]),
                        Name = row["Name"].ToString(),
                        LastName = row["LastName"].ToString(),
                        SecondLastName = row["SecondLastName"].ToString(),
                        Phone = row["Phone"].ToString(),
                        SpecialtyName = row["SpecialtyName"].ToString(),
                        FacultyName = row["FacultyName"].ToString(),
                        TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                        ProgramName = row["ProgramName"].ToString(),
                        Description = row["Description"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ObtenerInfoEstudiantePorTutor", ex);
            }
            return studentInfo;
        }


        /*INDICADOR FACULTAD*/
        public async Task<List<TutorContadorProgramasAcademicos>> ListarTutoresConCantidadDeProgramasMod(int facultyId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<TutorContadorProgramasAcademicos> tutores = new List<TutorContadorProgramasAcademicos>();

            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FacultyId", facultyId),
            new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
            new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_TUTORINGPROGRAMS_LISTARXTUTOR_SELECT_MOD", parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorContadorProgramasAcademicos tutor = new TutorContadorProgramasAcademicos
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["LastName"].ToString(),
                            TutorSecondLastName = row["SecondLastName"].ToString(),
                            CantidadProgramas = Convert.ToInt32(row["ProgramCount"])
                        };

                        tutores.Add(tutor);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarTutoresConCantidadDeProgramas", ex);
            }

            return tutores;
        }

        public async Task<List<ListarCantidadAppointment>> ListarCantidadAppointmentsMod(int facultyId, DateOnly? startDate = null, DateOnly? endDate = null)
        {
            List<ListarCantidadAppointment> appointments = new List<ListarCantidadAppointment>();

            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FacultyId", facultyId),
            new SqlParameter("@StartDate", startDate.HasValue ? startDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value),
            new SqlParameter("@EndDate", endDate.HasValue ? endDate.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value)
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_APPOINTMENTS_STATUS_COUNT_SELECT_MOD", parameters.ToArray());
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarCantidadAppointment appointment = new ListarCantidadAppointment
                        {
                            TutorId = Convert.ToInt32(row["TutorId"]),
                            TutorName = row["TutorName"].ToString(),
                            TutorLastName = row["LastName"].ToString(),
                            TutorSecondLastName = row["SecondLastName"].ToString(),
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
                throw new Exception("ERROR en ListarCantidadAppointments", ex);
            }

            return appointments;
        }




    }
}
