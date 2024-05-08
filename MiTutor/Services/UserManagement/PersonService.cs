using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.UserManagement
{
    public class PersonService
    {
        private readonly DatabaseManager _databaseManager;
        public PersonService()
        {
            _databaseManager = new DatabaseManager();
        }
        public async Task CrearPersona(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = person.Name },
                new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = person.LastName },
                new SqlParameter("@SecondLastName", SqlDbType.NVarChar) { Value = person.SecondLastName },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = person.Phone }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_PERSONA, parameters);
                person.Id = Convert.ToInt32(parameters[0].Value);
            }
            catch
            {
                throw new Exception("ERROR en Crear persona");
            }
        }
    }
}
