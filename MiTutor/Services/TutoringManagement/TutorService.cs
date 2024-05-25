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


        public TutorService()
        {
            _databaseManager = new DatabaseManager();
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

        public async Task<List<Tutor>> ListarTutores()
        {
            List<Tutor> tutores = new List<Tutor>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TUTORES, null);

                if (dataTable != null)
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
 
    }


}
