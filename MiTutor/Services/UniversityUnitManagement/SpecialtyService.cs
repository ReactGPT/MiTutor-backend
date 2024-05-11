using MiTutor.DataAccess;
using MiTutor.Models.UniversityUnitManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.UniversityUnitManagement
{
    public class SpecialtyService
    {
        private readonly DatabaseManager _databaseManager;


        public SpecialtyService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearEspecialidad(Specialty specialty)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = specialty.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = specialty.Acronym },
                new SqlParameter("@NumberOfStudents", SqlDbType.Int) { Value = specialty.NumberOfStudents },
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = specialty.Faculty.FacultyId }

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

    }
}
