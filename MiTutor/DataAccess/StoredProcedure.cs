namespace MiTutor.DataAccess
{
    public static class StoredProcedure
    {
        

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

        //PROGRAMA-TUTOR-TIPO_TUTOR
        public const string CREAR_PROGRAMA_TUTOR_TIPO_TUTOR = "TUTOR_PROGRAM_TYPE_INSERTAR_INSERT";
        public const string LISTAR_PROGRAMA_TUTOR_TIPO_TUTOR = "TUTOR_PROGRAM_TYPE_LISTAR_SELECT";

        //ESTUDIANTE-PROGRAMA
        public const string CREAR_ESTUDIANTE_PROGRAMA = "STUDENT_PROGRAM_INSERTAR_INSERT";
        public const string LISTAR_ESTUDIANTE_PROGRAMA = "STUDENT_PROGRAM_LISTAR_SELECT";
    }
}
