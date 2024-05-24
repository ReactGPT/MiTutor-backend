namespace MiTutor.DataAccess
{
    public static class StoredProcedure
    {
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

        //PERSONA
        public const string CREAR_PERSONA = "PERSON_INSERTAR_INSERT";

        //ESTUDIANTE
        public const string CREAR_ESTUDIANTE = "STUDENT_INSERTAR_INSERT";
        public const string LISTAR_ESTUDIANTES = "STUDENT_LISTAR_LIST";
        public const string LISTAR_ESTUDIANTES_POR_PROGRAMA = "STUDENT_LISTARXTUTORINGPROGRAMXTUTOR_SELECT";

        //USUARIO
        public const string CREAR_USUARIO = "USER_ACCOUNT_INSERTAR_INSERT";
        public const string LISTAR_USUARIOS = "USER_LISTAR_SELECT";

        //FACULTAD
        public const string CREAR_FACULTAD = "FACULTY_INSERTAR_INSERT";
        public const string LISTAR_FACULTADES = "FACULTY_LISTAR_SELECT";
        public const string ELIMINAR_FACULTAD = "FACULTY_ELIMINAR_DELETE";

        //ESPECIALIDAD
        public const string CREAR_ESPECIALIDAD = "SPECIALTY_INSERTAR_INSERT";
        public const string LISTAR_ESPECIALIDADES = "SPECIALTY_LISTAR_SELECT";
        public const string ELIMINAR_ESPECIALIDAD = "SPECIALTY_ELIMINAR_DELETE";

        //TUTOR
        public const string CREAR_TUTOR = "TUTOR_INSERTAR_INSERT";
        public const string LISTAR_TUTORES = "TUTOR_LISTAR_SELECT";

        //TUTORIA PROGRAM
        public const string CREAR_PROGRAMA_DE_TUTORIA = "TUTORINGPROGRAM_INSERTAR_INSERT";
        public const string LISTAR_PROGRAMA_DE_TUTORIA = "TUTORINGPROGRAM_LISTAR_LIST";

        public const string LISTAR_PROGRAMA_POR_TUTOR = "TUTORINGPROGRAM_LISTARXTUTOR_SELECT";
        public const string LISTAR_PROGRAMA_POR_TIPOUSUARIO = "TUTORINGPROGRAM_LISTARXTIPOUSUARIO_SELECT";
        public const string LISTAR_PROGRAMA_POR_ALUMNO = "TUTORINGPROGRAM_LISTARXALUMNO_SELECT";

        //PROGRAMA-TUTOR-TIPO_TUTOR
        public const string CREAR_PROGRAMA_TUTOR_TIPO_TUTOR = "TUTOR_PROGRAM_TYPE_INSERTAR_INSERT";
        public const string LISTAR_PROGRAMA_TUTOR_TIPO_TUTOR = "TUTOR_PROGRAM_TYPE_LISTAR_SELECT";

        //ESTUDIANTE-PROGRAMA-no funciona
        public const string CREAR_ESTUDIANTE_PROGRAMA = "STUDENT_PROGRAM_INSERTAR_INSERT";
        public const string LISTAR_ESTUDIANTE_PROGRAMA = "STUDENT_PROGRAM_LISTAR_SELECT";

        //COMENTARIO
        public const string CREAR_COMENTARIO = "COMMENT_INSERTAR_INSERT";
        public const string LISTAR_COMENTARIOS = "COMMENT_LISTAR_SELECT";
        public const string ACTUALIZAR_COMENTARIO = "COMMENT_ACTUALIZAR_UPDATE";
        public const string ELIMINAR_COMENTARIO = "COMMENT_ELIMINAR_DELETE";


        //FILE
        public const string CREAR_ARCHIVO = "FILE_INSERTAR_INSERT";
        public const string LISTAR_ARCHIVOS = "FILE_LISTAR_SELECT";
        public const string ACTUALIZAR_ARCHIVOS = "FILE_ACTUALIZAR_UPDATE";
        public const string ELIMINAR_ARCHIVO = "FILE_ELIMINAR_DELETE";
        
        //AGREGAR_CITA
        public const string AGREGAR_CITA = "APPOINTMENT_INSERTAR_INSERT";
        public const string OBTENER_ID_STUDENT_PROGRAM = "GetStudentProgramId";

        //LISTAR_CITA
        public const string LISTAR_CITA_POR_TUTOR = "APPOINTMENT_LISTARXTUTOR_SELECT";
        public const string LISTAR_CITA_POR_TUTOR_POR_ALUMNO = "APPOINTMENT_LISTARXTUTORXALUMNO_SELECT";


        //DERIVATION
        public const string CREAR_DERIVACION = "DERIVATION_INSERTAR_INSERT";
        public const string LISTAR_DERIVACIONES = "DERIVATION_LISTAR_SELECT";
        public const string ACTUALIZAR_DERIVACION = "DERIVATION_ACTUALIZAR_UPDATE";
        public const string ELIMINAR_DERIVACION = "DERIVATION_ELIMINAR_DELETE";

        //RESULTADO-CITA CON COMENTARIOS
        public const string INSERTAR_RESULTADO_CITA= "APPOINTMENTRESULT_INSERTAR_INSERT"; 
        public const string ACTUALIZAR_RESULTADO_CITA = "APPOINTMENTRESULT_ACTUALIZAR_UPDATE";
        public const string CONSULTAR_RESULTADO_CITA = "APPOINTMENTRESULT_CONSULTAR";
        public const string CONSULTAR_COMENTARIOS_X_ID_RESULTADO_CITA = "COMMENT_CONSULTAR_POR_ID_APPOINMENT_RESULT";
        public const string ACTUALIZAR_COMMENT_X_ID = "COMMENT_ACTUALIZAR_X_ID_UPDATE";

        //DISPONIBILIDAD TUTOR
        public const string LISTAR_DISPONIBILIDADES_X_TUTOR = "AVAILABILITYTUTOR_LISTARXTUTOR_SELECT";
        public const string ELIMINAR_DISPONIBILIDAD = "AVAILABILITYTUTOR_ELIMINAR_DELETE";
        public const string CREAR_DISPONIBILIDAD = "AVAILABILITYTUTOR_INSERTAR_INSERT";
    }
}
