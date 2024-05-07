using MiTutor.DataAccess;
using MiTutor.Models.GestionUsuarios;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.TutoringManagement;

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

    }
}
