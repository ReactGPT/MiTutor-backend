namespace MiTutor.DataAccess
{
    public static class StoredProcedure
    {

        //PERSONA
        public const string CREAR_PERSONA = "PERSON_INSERTAR_INSERT";

        //ESTUDIANTE
        public const string CREAR_ESTUDIANTE = "STUDENT_INSERTAR_INSERT";
        public const string LISTAR_ESTUDIANTES = "STUDENT_LISTAR_LIST";

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


        //DERIVATION
        public const string CREAR_DERIVACION = "DERIVATION_INSERTAR_INSERT";
        public const string LISTAR_DERIVACIONES = "DERIVATION_LISTAR_SELECT";
        public const string ACTUALIZAR_DERIVACION = "DERIVATION_ACTUALIZAR_UPDATE";
        public const string ELIMINAR_DERIVACION = "DERIVATION_ELIMINAR_DELETE";

    }
}
