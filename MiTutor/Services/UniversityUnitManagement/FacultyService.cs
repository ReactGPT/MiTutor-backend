using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Services.UniversityUnitManagement
{
    public class FacultyService
    {
        private readonly DatabaseManager _databaseManager;

        public FacultyService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearFacultad(Faculty facultad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = facultad.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = facultad.Acronym }, 
                new SqlParameter("@NumberOfStudents", SqlDbType.Int) { Value = facultad.NumberOfStudents },
                new SqlParameter("@NumberOfTutors", SqlDbType.Int) { Value = facultad.NumberOfTutors }  
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_FACULTAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la facultad: " + ex.Message);
            }
        }

        public async Task<List<Faculty>> ListarFacultades()
        {
            List<Faculty> facultades = new List<Faculty>();

            try
            {
                // Ejecutar procedimiento almacenado para obtener las facultades
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_FACULTADES);

                // Verificar si se obtuvieron datos
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado y crear objetos Faculty
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Faculty facultad = new Faculty
                        {
                            FacultyId = Convert.ToInt32(row["FacultyId"]),
                            Name = row["Name"].ToString(),
                            Acronym = row["Acronym"].ToString(),
                            NumberOfStudents = Convert.ToInt32(row["NumberOfStudents"]),
                            NumberOfTutors = Convert.ToInt32(row["NumberOfTutors"]),
                        };
                        if (row["FacultyManagerId"] != DBNull.Value)
                        {
                            facultad.FacultyManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["FacultyManagerId"]),
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["LastName"].ToString()
                                }
                            };
                            
                        };

                        facultades.Add(facultad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facultades: " + ex.Message);
            }

            return facultades;
        }

        public async Task<List<Faculty>> ListarFacultadesTodos()
        {
            List<Faculty> facultades = new List<Faculty>();

            try
            {
                // Ejecutar procedimiento almacenado para obtener las facultades
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_FACULTADES_TODOS);

                // Verificar si se obtuvieron datos
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado y crear objetos Faculty
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Faculty facultad = new Faculty
                        {
                            FacultyId = Convert.ToInt32(row["FacultyId"]),
                            Name = row["Name"].ToString(),
                            Acronym = row["Acronym"].ToString(),
                            NumberOfStudents = Convert.ToInt32(row["NumberOfStudents"]),
                            NumberOfTutors = Convert.ToInt32(row["NumberOfTutors"]),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                        };
                        if (row["FacultyManagerId"] != DBNull.Value)
                        {
                            facultad.FacultyManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["FacultyManagerId"]),
                                InstitutionalEmail = row["InstitutionalEmail"].ToString(), // Agregar el correo electrónico del gerente de facultad
                                PUCPCode = row["PUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["PersonName"].ToString(),
                                    LastName = row["LastName"].ToString()
                                }
                            };
                        };
                        if (row["BienestarManagerId"] != DBNull.Value)
                        {
                            facultad.BienestarManager = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["BienestarManagerId"]),
                                InstitutionalEmail = row["BienestarInstitutionalEmail"].ToString(),
                                PUCPCode = row["BienestarPUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["BienestarName"].ToString(),
                                    LastName = row["BienestarLastName"].ToString()
                                }
                            };
                        };
                        if (row["PersonalApoyoId"] != DBNull.Value)
                        {
                            facultad.PersonalApoyo = new Models.GestionUsuarios.UserAccount
                            {
                                Id = Convert.ToInt32(row["PersonalApoyoId"]),
                                InstitutionalEmail = row["PersonalApoyoInstitutionalEmail"].ToString(),
                                PUCPCode = row["PersonalApoyoPUCPCode"].ToString(),
                                Persona = new Models.GestionUsuarios.Person
                                {
                                    Name = row["PersonalApoyoName"].ToString(),
                                    LastName = row["PersonalApoyoLastName"].ToString()
                                }
                            };
                        };

                        facultades.Add(facultad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facultades: " + ex.Message);
            }

            return facultades;
        }

        public async Task ActualizarFacultad(Faculty facultad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = facultad.FacultyId },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = facultad.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = facultad.Acronym },
                new SqlParameter("@NumberOfStudents", SqlDbType.Int) { Value = facultad.NumberOfStudents },
                new SqlParameter("@NumberOfTutors", SqlDbType.Int) { Value = facultad.NumberOfTutors },
                new SqlParameter("@FacultyManagerId", SqlDbType.Int) { Value = (object)facultad.FacultyManager.Id ?? DBNull.Value },
                new SqlParameter("@BienestarManagerId", SqlDbType.Int) { Value = (object)facultad.BienestarManager.Id ?? DBNull.Value },
                new SqlParameter("@PersonalApoyoId", SqlDbType.Int) { Value = (object)facultad.PersonalApoyo.Id ?? DBNull.Value },
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_FACULTAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la facultad: " + ex.Message);
            }
        }

        public async Task EliminarFacultad(int facultadId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = facultadId },
            };
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_FACULTAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en EliminarFacultad");
            }
       }

    }
}
