using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services
{
    public class ActionPlanService
    {
        private readonly DatabaseManager _databaseManager;

        public ActionPlanService()
        {
            _databaseManager = new DatabaseManager();
        }


        public async Task CrearActionPlan(ActionPlan actionPlan)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = actionPlan.Name },
                new SqlParameter("@Description", SqlDbType.VarChar) {Value = actionPlan.Description},
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = actionPlan.StudentId },
                new SqlParameter("@ProgramId", SqlDbType.Int) { Value = actionPlan.ProgramId },
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = actionPlan.TutorId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_ACTION_PLAN, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearPlanesAcciones");
            }
        }

        public async Task<List<ActionPlan>> ListarActionPlans(int studentId, int programId, int tutorId)
        {


            List<ActionPlan> actionPlans = new List<ActionPlan>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId},
                new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = programId},
                new SqlParameter("@TutorId", SqlDbType.Int) {Value = tutorId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PLAN_ACTIONS, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ActionPlan actionPlan = new ActionPlan
                        {
                            ActionPlanId = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Description = row[2].ToString(),
                            IsActive = Convert.ToBoolean(row[3]),
                            StudentProgramId = Convert.ToInt32(row[4]),
                            TutorId = Convert.ToInt32(row[5]),
                            CreationDate = Convert.ToDateTime(row[6]),
                            ModificationDate = Convert.ToDateTime(row[7]),

                        };
                        actionPlans.Add(actionPlan);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarActionPlans", ex);
            }

            return actionPlans;
        }

        public async Task<List<ActionPlan>> ListarActionPlansStudent(int studentId, int programId, int tutorId)
        {


            List<ActionPlan> actionPlans = new List<ActionPlan>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId},
                new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = programId},
                new SqlParameter("@TutorId", SqlDbType.Int) {Value = tutorId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ACTION_PLAN_ALUMNO, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ActionPlan actionPlan = new ActionPlan
                        {
                            ActionPlanId = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Description = row[2].ToString(),
                            IsActive = Convert.ToBoolean(row[3]),
                            StudentProgramId = Convert.ToInt32(row[4]),
                            TutorId = Convert.ToInt32(row[5]),
                            CreationDate = Convert.ToDateTime(row[6]),
                            ModificationDate = Convert.ToDateTime(row[7]),

                        };
                        actionPlans.Add(actionPlan);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarActionPlansStudent", ex);
            }

            return actionPlans;
        }

        public async Task<List<ActionPlan>> ListarActionPlansPorId(int ActionPlanId)
        {
            List<ActionPlan> actionPlans = new List<ActionPlan>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionPlanId", SqlDbType.Int) { Value = ActionPlanId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.OBTENER_ACTION_PLAN_X_ID, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ActionPlan actionPlan = new ActionPlan
                        {
                            ActionPlanId = Convert.ToInt32(row[0]),
                            Name = row[1].ToString(),
                            Description = row[2].ToString(),
                            IsActive = Convert.ToBoolean(row[3]),
                            StudentProgramId = Convert.ToInt32(row[4]),
                            TutorId = Convert.ToInt32(row[5]),
                            CreationDate = Convert.ToDateTime(row[6]),
                            ModificationDate = Convert.ToDateTime(row[7]),

                        };
                        actionPlans.Add(actionPlan);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarActionPlans", ex);
            }

            return actionPlans;
        }

        public async Task ActualizarPlan(ActionPlan actionPlan)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionPlanId", SqlDbType.Int) { Value = actionPlan.ActionPlanId },
                new SqlParameter("@Name", SqlDbType.VarChar) {Value = actionPlan.Name},
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = actionPlan.Description },
                new SqlParameter("@IsActive", SqlDbType.Int) { Value = actionPlan.IsActive }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_PLAN, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarPlandeActionService");
            }
        }

        public async Task EliminarActionPlan(int actionPlanId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ActionPlanId", SqlDbType.Int) { Value = actionPlanId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_ACTION_PLAN, parameters);
            }
            catch
            {
                throw new Exception("ERROR en EliminarActionPlan");
            }
        }
    }
}
