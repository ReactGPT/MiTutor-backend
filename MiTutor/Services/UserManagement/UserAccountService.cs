using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Services.GestionUsuarios;
using Microsoft.AspNetCore.Mvc;

namespace MiTutor.Services.UserManagement
{
    public class UserAccountService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly PersonService _personService;
        public UserAccountService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
            _personService = new PersonService(_databaseManager);
        }

        public async Task CrearUsuario(UserAccount userAccount)
        {
            // Crear Usuario
            if(userAccount.Id == -1)
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
                    userAccount.Id = userAccount.Persona.Id;
                }
                catch
                {
                    throw new Exception("ERROR en CrearUsuario");
                }
            } else
            {
                // Editar usuario
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@UserAccountId", SqlDbType.Int) { Value= userAccount.Persona.Id },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = userAccount.Persona.Phone },
                new SqlParameter("@InstitutionalEmail", SqlDbType.NVarChar) { Value = userAccount.InstitutionalEmail },
                new SqlParameter("@PUCPCode", SqlDbType.NVarChar) { Value = userAccount.PUCPCode},
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = userAccount.Persona.Name },
                new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = userAccount.Persona.LastName },
                new SqlParameter("@SecondLastName", SqlDbType.NVarChar) { Value = userAccount.Persona.SecondLastName },
                new SqlParameter("@UserAccountIsActive", SqlDbType.Bit) { Value = userAccount.IsActive }
                };
                try
                {
                    await _databaseManager.ExecuteStoredProcedure(StoredProcedure.EDITAR_USUARIO, parameters);
                }
                catch
                {
                    throw new Exception("ERROR en EditarUsuario");
                }
            }
        }

        public async Task EliminarUsuario(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@PersonId", SqlDbType.Int) { Value = id }
            };
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_USUARIO, parameters);
            }
            catch
            {
                throw new Exception("Error en EliminarUuario");
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
                            CreationDate = Convert.ToDateTime(row["CreationDate"]).Date,
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]).Date,
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
                            case "FACULTYMANAGER":
                            case "SPECIALTYMANAGER":
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
                            case "DERIVATION":
                                userAccount.Roles.Add(new UserDerivation
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsDerivation = true
                                });
                                break;
                            case "CAREMANAGER":
                                userAccount.Roles.Add(new UserCaringManager
                                {
                                    Id = Convert.ToInt32(row["UserAccountId"]),
                                    AccountTypeId = Convert.ToInt32(row["UserAccountTypeId"]),
                                    RolName = row["Description"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsCaringManager = true,
                                    FacultyId= Convert.ToInt32(row["BienestarFacId"]),
                                    FacultyName = row["BienestarFacName"].ToString()
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

        public async Task<List<UserAccount>> ListarUsuariosSinAlumnos()
        {
            List<UserAccount> usuarios = new List<UserAccount>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_USUARIOS_SIN_ALUMNOS, null);
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
                            CreationDate = Convert.ToDateTime(row["CreationDate"]).Date,
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]).Date,
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

        public async Task<List<AccountType>> ListarTiposCuenta(int userId)
        {
            List<AccountType> tiposCuenta = new List<AccountType>();
            
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_TIPOSCUENTA_TODOS, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        AccountType tipoCuenta = new AccountType
                        {
                            Id = Convert.ToInt32(row["UserAccountTypeId"]),
                            description = row["Description"].ToString(),
                        };

                        tiposCuenta.Add(tipoCuenta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en Listar Tipos de cuenta: " + ex.Message);
            }
            return tiposCuenta;
        }
        public async Task ModificarRolUsuario(int userId , List<AccountType> roles, int facultyId, int specialtyId,int unitDerivationId)
        {
            foreach(AccountType rol in roles)
            {
                try
                {
                    SqlParameter[] parameters=new SqlParameter[]
                    {

                    };
                    switch (rol.Id)
                    {
                        case 1: //Adm
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId}
                            };
                            break;
                        case 2: //Coord Facultad
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId},
                                new SqlParameter("@idFaculty",SqlDbType.Int) {Value = facultyId}
                            };
                            break;
                        case 3://Coord Specialidad
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId},
                                new SqlParameter("@@idSpecialty",SqlDbType.Int) {Value = specialtyId}
                            };
                            break;
                        case 4: //Alumno
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId},
                                new SqlParameter("@idSpecialty",SqlDbType.Int) {Value = specialtyId}
                            };
                            break;
                        case 5://Tutor
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId}
                            };
                            break;
                        case 11:
                            parameters = new SqlParameter[]
                            {
                                new SqlParameter("@idRol",SqlDbType.Int) { Value = rol.Id },
                                new SqlParameter("idUser",SqlDbType.Int) {Value = userId},
                                new SqlParameter("@@idDerivationUnit",SqlDbType.Int) {Value = unitDerivationId}
                            };
                            break;
                    }
                    await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_ROL_USUARIO, parameters);
                }
                catch(Exception ex)
                {
                    throw new Exception("ERROR en Crear Rol: " + ex.Message);
                }
                
            }
            return;
        }

        public async Task<List<UserAccount>> validateUserByEmail(string email)
        {
            List<UserAccount> usuarios = new List<UserAccount>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InstitutionalEmail", SqlDbType.NVarChar) { Value = email }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.VALIDAR_CORREO_USUARIO, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserAccount userAccount = new UserAccount
                        {
                            Id = Convert.ToInt32(row["UserAccountId"]),
                            InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                            PUCPCode = row["PUCPCode"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            CreationDate = Convert.ToDateTime(row["CreationDate"]).Date,
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]).Date
                        };
                        usuarios.Add(userAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUsuariosValidacionCorreo: " + ex.Message);
            }

            return usuarios;
        }

        public async Task<List<UserAccount>> validateUserByPUCPCode(string pucpCode)
        {
            List<UserAccount> usuarios = new List<UserAccount>();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PUCPCode", SqlDbType.NVarChar) { Value = pucpCode }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.VALIDAR_CODIGO_USUARIO, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UserAccount userAccount = new UserAccount
                        {
                            Id = Convert.ToInt32(row["UserAccountId"]),
                            InstitutionalEmail = row["InstitutionalEmail"].ToString(),
                            PUCPCode = row["PUCPCode"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            CreationDate = Convert.ToDateTime(row["CreationDate"]).Date,
                            ModificationDate = Convert.ToDateTime(row["ModificationDate"]).Date
                        };
                        usuarios.Add(userAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUsuariosValidacionCODIGO: " + ex.Message);
            }

            return usuarios;
        }
    }
}
