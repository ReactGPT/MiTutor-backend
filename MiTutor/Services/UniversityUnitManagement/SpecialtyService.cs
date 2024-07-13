using MiTutor.DataAccess;
using MiTutor.Models.UniversityUnitManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Controllers.UniversityUnitManagement;

namespace MiTutor.Services.UniversityUnitManagement
{
    public class SpecialtyService
    {
        private readonly DatabaseManager _databaseManager;
        
        public SpecialtyService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearEspecialidad(Specialty specialty)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = specialty.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = specialty.Acronym },
                new SqlParameter("@NumberOfStudents", SqlDbType.Int) { Value = specialty.NumberOfStudents },
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = specialty.Faculty.FacultyId },
                new SqlParameter("@ManagerId", SqlDbType.Int) { Value = (object)specialty.SpecialtyManager.Id ?? DBNull.Value }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_ESPECIALIDAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la facultad: " + ex.Message);
            }
        }

        public async Task ModificarEspecialidad(Specialty specialty)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = specialty.SpecialtyId },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = specialty.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = specialty.Acronym },
                new SqlParameter("@ManagerId", SqlDbType.Int) { Value = (object)specialty.SpecialtyManager.Id ?? DBNull.Value },
                new SqlParameter("@PersonalApoyoId", SqlDbType.Int) { Value = (object)specialty.PersonalApoyo.Id ?? DBNull.Value },
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.MODIFICAR_ESPECIALIDAD, parameters);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la especialidad: " + ex.Message);
            }
        }

        public async Task<List<Specialty>> ListarEspecialidades()
        {
            List<Specialty> specialties = new List<Specialty>();

            try
            {
                // Ejecutar procedimiento almacenado para obtener las facultades
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESPECIALIDADES);

                // Verificar si se obtuvieron datos
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado y crear objetos Faculty
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Specialty specialty = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                            Name = row["Name"].ToString(),
                            Acronym = row["Acronym"].ToString(),
                            NumberOfStudents = Convert.ToInt32(row["NumberOfStudents"]),
                            CreationDate = Convert.ToDateTime(row["CreationDate"]),
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            Faculty = new Faculty
                            {
                                FacultyId = Convert.ToInt32(row["FacultyId"]),
                                Name = row["FacultyName"].ToString(),
                                Acronym = row["FacultyAcronym"].ToString()
                            }
                        };

                        if (row["SpecialtyManagerId"] != DBNull.Value)
                        {
                            specialty.SpecialtyManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["SpecialtyManagerId"]),
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["ManagerName"].ToString(),
                                    LastName = row["ManagerLastName"].ToString(),
                                    Phone = row["Phone"].ToString()
                                }
                            };
                        }; 
                        specialties.Add(specialty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facultades: " + ex.Message);
            }

            return specialties;
        }

        public async Task<List<Specialty>> ListarEspecialidadesPorFacultad(int FacultyId)
        {
            List<Specialty> specialties = new List<Specialty>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FacultyId", SqlDbType.Int) { Value = FacultyId },
                };

                // Ejecutar procedimiento almacenado para obtener las facultades
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESPECIALIDAD_X_FACULTAD, parameters);

                // Verificar si se obtuvieron datos
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado y crear objetos Faculty
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Specialty specialty = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                            Name = row["Name"].ToString(),
                            Acronym = row["Acronym"].ToString(),
                            NumberOfStudents = Convert.ToInt32(row["NumberOfStudents"]),
                            Faculty = new Faculty
                            {
                                FacultyId = Convert.ToInt32(row["FacultyId"]),
                                //Name = row["FacultyName"].ToString(),
                                //Acronym = row["FacultyAcronym"].ToString()
                            }
                        };

                        if (row["SpecialtyManagerId"] != DBNull.Value)
                        {
                            specialty.SpecialtyManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["SpecialtyManagerId"]),
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["ManagerName"].ToString(),
                                    LastName = row["ManagerLastName"].ToString()
                                }
                            };
                        };
                        specialties.Add(specialty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facultades: " + ex.Message);
            }

            return specialties;
        }

        public async Task<List<Specialty>> ListarEspecialidadesXNombre(string specialtyName)
        {
            List<Specialty> specialties = new List<Specialty>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = specialtyName }
            };
            try
            {
                // Ejecutar procedimiento almacenado para obtener las facultades
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESPECIALIDADESXNOMBRE, parameters);

                // Verificar si se obtuvieron datos
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado y crear objetos Faculty
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Specialty specialty = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                            Name = row["Name"].ToString(),
                            Acronym = row["Acronym"].ToString(),
                            NumberOfStudents = Convert.ToInt32(row["NumberOfStudents"]),
                            CreationDate = Convert.ToDateTime(row["CreationDate"]),
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            Faculty = new Faculty
                            {
                                FacultyId = Convert.ToInt32(row["FacultyId"]),
                                Name = row["FacultyName"].ToString(),
                                Acronym = row["FacultyAcronym"].ToString(),

                            }
                        };

                        if (row["SpecialtyManagerId"] != DBNull.Value)
                        {
                            specialty.SpecialtyManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["SpecialtyManagerId"]),
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["ManagerName"].ToString(),
                                    LastName = row["ManagerLastName"].ToString(),
                                    Phone = row["Phone"].ToString()
                                }
                            };
                        };

                        if (row["PersonalApoyoId"] != DBNull.Value)
                        {
                            specialty.PersonalApoyo = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["PersonalApoyoId"]),
                                InstitutionalEmail = row["PersonalApoyoInstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PersonalApoyoPUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["PersonalApoyoName"].ToString(),
                                    LastName = row["PersonalApoyoLastName"].ToString(),
                                    Phone = row["PersonalApoyoPhone"].ToString()
                                }
                            };
                        };

                        specialties.Add(specialty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facultades: " + ex.Message);
            }

            return specialties;
        }

        public async Task EliminarEspecialidad(int SpecialtyId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = SpecialtyId },
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_ESPECIALIDAD, parameters);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la facultad: " + ex.Message);
            }
        }
    }
}
