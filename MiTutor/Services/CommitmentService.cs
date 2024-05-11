using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace MiTutor.Services
{
    public class CommitmentService
    {
        private readonly DatabaseManager _databaseManager;

        public CommitmentService()
        {
            _databaseManager = new DatabaseManager();
        }


        public async Task<List<Commitment>> ListarCommitmentPorIdPlanAction(int actionPlanId)
        {
            List<Commitment> commitments = new List<Commitment>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionPlanId", SqlDbType.Int) { Value = actionPlanId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_COMMITMENT_X_ID_ACTION_PLAN, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Commitment commitment = new Commitment
                        {
                            CommitmentId = Convert.ToInt32(row[0]),
                            Description = Convert.ToString(row[1]),
                            IsActive = Convert.ToBoolean(row[2]),
                            ActionPlanId = Convert.ToInt32(row[3]),
                            CommitmentStatusId = Convert.ToInt32(row[4]),
                            CommitmentStatusDescription = Convert.ToString(row[5]),
                            CreationDate = Convert.ToDateTime(row[6]),
                            ModificationDate = Convert.ToDateTime(row[7])

                        };
                        commitments.Add(commitment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarCommitments", ex);
            }

            return commitments;
        }

        public async Task CrearCommitment(int actionPlanId, string description)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionPlanId", SqlDbType.Int) { Value = actionPlanId },
                new SqlParameter("@Description", SqlDbType.VarChar) {Value = description}
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_COMMITMENT, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearCommitment");
            }
        }

        public async Task ActualizarCommitment(Commitment commitment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CommitmentId", SqlDbType.Int) { Value = commitment.CommitmentId },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = commitment.Description },
                new SqlParameter("@CommitmentStatusId", SqlDbType.Int) { Value = commitment.CommitmentStatusId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_COMMITMENT, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarCommitmentService");
            }
        }


        public async Task EliminarCommitment(int commitmentId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CommitmentId", SqlDbType.Int) { Value = commitmentId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.DELETE_COMMITMENT, parameters);
            }
            catch
            {
                throw new Exception("ERROR en EliminarCommitment");
            }
        }

    }
}
