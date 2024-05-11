namespace MiTutor.DataAccess
{
    public static class StoredProcedure
    {
        public const string CREAR_ESPECIALIDAD = "ESP_CrearEspecialidad_Insert";
        public const string LISTAR_ESPECIALIDAD = "ESP_ListarEspecialidades_Select";
        public const string ACTUALIZAR_ESPECIALIDAD = "ESP_ActualizarEspecialidad_Update";
        public const string LISTAR_PLAN_ACTIONS = "sp_GetActionPlans";
        public const string CREAR_ACTION_PLAN = "sp_InsertActionPlan";
        public const string OBTENER_ACTION_PLAN_X_ID = "sp_GetActionPlansById";
        public const string LISTAR_COMMITMENT_X_ID_ACTION_PLAN = "sp_GetCommitmentsByActionPlanId";
        public const string INSERTAR_COMMITMENT = "sp_InsertCommitment";
        public const string ACTUALIZAR_PLAN = "sp_UpdateActionPlan";
        public const string ACTUALIZAR_COMMITMENT = "sp_UpdateCommitment";
        public const string DELETE_COMMITMENT = "sp_DeleteCommitment";
        public const string ELIMINAR_ACTION_PLAN = "sp_DeleteActionPlan";
    }
}
