using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.UserManagement
{
    public class UserAccountService
    {
        private readonly DatabaseManager _databaseManager;

        public UserAccountService()
        {
            _databaseManager = new DatabaseManager();
        }
        public async Task CrearUsuario(UserAccount userAccount)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                //new SqlParameter("@UserAccountId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = userAccount.Persona.Name },
                new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = userAccount.Persona.LastName },
                new SqlParameter("@SecondLastName", SqlDbType.NVarChar) { Value = userAccount.Persona.SecondLastName },
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
