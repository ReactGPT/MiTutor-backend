using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Services.GestionUsuarios;

namespace MiTutor.Services.UserManagement
{
    public class UserAccountService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly PersonService _personService;
        private readonly StudentService _studentService;
        public UserAccountService()
        {
            _databaseManager = new DatabaseManager();
            _personService = new PersonService();
            _studentService = new StudentService();
        }
        public async Task CrearUsuario(UserAccount userAccount)
        {
            await _personService.CrearPersona(userAccount.Persona);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserAccountId", SqlDbType.Int) { Value= userAccount.Persona.Id },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = userAccount.Persona.Phone },
                new SqlParameter("@InstitutionalEmail", SqlDbType.NVarChar) { Value = userAccount.InstitutionalEmail },
                new SqlParameter("@PUCPCode", SqlDbType.NVarChar) { Value = userAccount.PUCPCode}
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_USUARIO, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearUsuario");
            }
        }

        public async Task<List<UserAccount>> ListarUsuarios()
        {
            List<UserAccount> usuarios = new List<UserAccount>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_USUARIOS, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserAccount userAccount = new UserAccount
                        {
                            Id = Convert.ToInt32(row["PersonId"]),
                            InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                            PUCPCode = row["PUCPCode"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            Persona = new Person
                            {
                                Id = Convert.ToInt32(row["PersonId"]),
                                Name = row["Name"].ToString(),
                                LastName = row["LastName"].ToString(),
                                SecondLastName = row["SecondLastName"].ToString(),
                                Phone = row["Phone"].ToString(),
                                IsActive = Convert.ToBoolean(row["IsActive"])
                            }
                        };

                        usuarios.Add(userAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUsuarios: " + ex.Message);
            }

            return usuarios;
        }

    }
}
