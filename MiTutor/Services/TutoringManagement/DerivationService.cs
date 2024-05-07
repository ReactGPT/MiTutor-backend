using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.TutoringManagement
{
    public class DerivationService
    {
        private readonly DatabaseManager _databaseManager;


        public DerivationService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearDerivacion(Derivation derivation)
        {
            // Convertir DateOnly a DateTime
            DateTime creationDate = DateTime.Parse(derivation.CreationDate.ToString());

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Reason", SqlDbType.NVarChar) { Value = derivation.Reason },
                new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = derivation.Comment },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = derivation.Status },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = creationDate },
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = derivation.UnitDerivationId },
                new SqlParameter("@UserAccountId", SqlDbType.Int) { Value = derivation.UserAccountId },
                new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = derivation.AppointmentId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_DERIVACION, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en CrearDerivacion", ex);
            }
        }

        public async Task<List<Derivation>> ListarDerivations()
        {
            List<Derivation> derivations = new List<Derivation>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_DERIVACIONES, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime dateTime = Convert.ToDateTime(row["CreationDate"]);
                        DateOnly creationDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day); // Crear DateOnly a partir de DateTime
                        Derivation derivation = new Derivation
                        {
                            DerivationId = Convert.ToInt32(row["DerivationId"]),
                            Reason = row["Reason"].ToString(),
                            Comment = row["Comment"].ToString(),
                            Status = row["Status"].ToString(),
                            CreationDate = creationDate,
                            UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]),
                            UserAccountId = Convert.ToInt32(row["UserAccountId"]),
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        derivations.Add(derivation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarDerivaciones", ex);
            }

            return derivations;
        }
        public async Task ActualizarDerivacion(Derivation derivation)
        {
            // Convertir DateOnly a DateTime
            DateTime creationDate = DateTime.Parse(derivation.CreationDate.ToString());
            SqlParameter[] parameters = new SqlParameter[]
            {

                new SqlParameter("@DerivationId", SqlDbType.Int) { Value = derivation.DerivationId },
                new SqlParameter("@Reason", SqlDbType.NVarChar) { Value = derivation.Reason },
                new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = derivation.Comment },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = derivation.Status },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = creationDate },
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = derivation.UnitDerivationId },
                new SqlParameter("@UserAccountId", SqlDbType.Int) { Value = derivation.UserAccountId },
                new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = derivation.AppointmentId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_DERIVACION, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ActualizarDerivacionService", ex);
            }
        }

        public async Task EliminarDerivacion(int derivationId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DerivationId", SqlDbType.Int) { Value = derivationId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_DERIVACION, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en EliminarDerivacion", ex);
            }
        }

    }
}
