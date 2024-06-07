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

        public async Task<UserAccount> ObtenerInfoUsuario(string email,string codigPUCP)
        {
            //List<UserAccount> usuarios = new List<UserAccount>();
            UserAccount userAccount = new UserAccount
            {
                isVerified = false
            };
            try
            {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    email==null?new SqlParameter("@codigoPUCPToVerify", SqlDbType.NVarChar) { Value = codigPUCP }:new SqlParameter("@emailToVerify", SqlDbType.NVarChar) { Value= email }
                };
                //if (email == null)
                //{
                //    SqlParameter[] parameters = new SqlParameter[]
                //    {
                //        new SqlParameter("@codigoPUCPToVerify", SqlDbType.NVarChar) { Value = codigPUCP }
                //    };
                //}
                //else if(codigPUCP == null)
                //{
                //    SqlParameter[] parameters = new SqlParameter[]
                //    {
                //        new SqlParameter("@emailToVerify", SqlDbType.NVarChar) { Value= email }
                //    };
                //}
                

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.OBTENERINFO_USUARIO, parameters);
                if (dataTable != null && dataTable.Rows.Count>0)
                {
                    userAccount = new UserAccount
                    {
                        isVerified=true,
                        Id = Convert.ToInt32(dataTable.Rows[0]["UserAccountId"]),
                        InstitutionalEmail = dataTable.Rows[0]["InstitutionalEmail"].ToString(),
                        PUCPCode = dataTable.Rows[0]["PUCPCode"].ToString(),
                        IsActive = true,
                        Persona = new Person
                        {
                            Id = Convert.ToInt32(dataTable.Rows[0]["PersonId"]),
                            Name = dataTable.Rows[0]["Name"].ToString(),
                            LastName = dataTable.Rows[0]["LastName"].ToString(),
                            SecondLastName = dataTable.Rows[0]["SecondLastName"].ToString(),
                            Phone = dataTable.Rows[0]["Phone"].ToString(),
                            IsActive = true
                        },
                        Roles = new List<object>()
                    };
                    foreach (DataRow row in dataTable.Rows)
                    {

                        //UserGeneric userRol= null;
                        switch (row["Type"].ToString())
                        {
                            case "MANAGER":
                                //userRol = 
                                userAccount.Roles.Add(new UserGenericManager
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type= row["Type"].ToString(),
                                    IsManager = true,
                                    ManagerId= Convert.ToInt32(row["UserAccountId"]),
                                    DepartmentType = row["DepartmentType"].ToString(),
                                    DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                                    DepartmentName = row["DepartmentName"].ToString(),
                                    DepartmentAcronym = row["DepartmentAcronym"].ToString()
                                });
                                break;
                            case "TUTOR":
                                //userRol = 
                                    userAccount.Roles.Add(new UserGenericTutor
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsTutor = true,
                                    MeetingRoom = row["MeetingRoom"].ToString(),
                                    TutorId = Convert.ToInt32(row["TutorId"])
                                });
                                break;
                            case "STUDENT":
                                //userRol = 
                                userAccount.Roles.Add(new UserStudent
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsStudent = true,
                                    IsRisk = Convert.ToBoolean(row["IsRisk"]),
                                    SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                                    SpecialtyName = row["SpecialtyName"].ToString(),
                                    FacultyId = Convert.ToInt32(row["FacultyId"]),
                                    FacultyName = row["FacultyName"].ToString()
                                });
                                break;
                            case "ADMIN":
                                userAccount.Roles.Add(new UserAdmin
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsAdmin= true
                                });
                                break;
                            default:
                                //userRol = null;
                                break;
                        }

                        //userAccount.Roles.Add(userRol);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUsuarios: " + ex.Message);
            }

            return userAccount;
        }

    }
}
