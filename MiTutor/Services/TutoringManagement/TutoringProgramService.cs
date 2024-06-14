using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using MiTutor.Models.UniversityUnitManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.GestionUsuarios;
using System.ComponentModel;

namespace MiTutor.Services.TutoringManagement
{
    public class TutoringProgramService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly TutorService _tutorServices;

        public TutoringProgramService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearProgramaDeTutoria(TutoringProgram programa)
        {
            SqlParameter[] parameters;


                if (programa.Faculty != null)
            {
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@FaceToFace", SqlDbType.Bit) { Value = programa.FaceToFace },
                    new SqlParameter("@Virtual", SqlDbType.Bit) { Value = programa.Virtual },
                    new SqlParameter("@GroupBased", SqlDbType.Bit) { Value = programa.GroupBased },
                    new SqlParameter("@IndividualBased", SqlDbType.Bit) { Value = programa.IndividualBased },
                    new SqlParameter("@Optional", SqlDbType.Bit) { Value = programa.Optional },
                    new SqlParameter("@Mandatory", SqlDbType.Bit) { Value = programa.Mandatory },
                    new SqlParameter("@MembersCount", SqlDbType.Int) { Value = programa.MembersCount },
                    new SqlParameter("@ProgramName", SqlDbType.NVarChar) { Value = programa.ProgramName },
                    new SqlParameter("@Description", SqlDbType.NVarChar) { Value = programa.Description },
                    new SqlParameter("@Duration", SqlDbType.Time) { Value = programa.Duration },
                    new SqlParameter("@FacultyId", SqlDbType.Int) { Value = programa.Faculty.FacultyId },
                    new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = DBNull.Value }
                };
            }
            else
            {
                 // Si programa.Faculty es null, asignamos un valor DBNull.Value al parámetro @FacultyId
                 parameters = new SqlParameter[]
                  {
                        new SqlParameter("@FaceToFace", SqlDbType.Bit) { Value = programa.FaceToFace },
                        new SqlParameter("@Virtual", SqlDbType.Bit) { Value = programa.Virtual },
                        new SqlParameter("@GroupBased", SqlDbType.Bit) { Value = programa.GroupBased },
                        new SqlParameter("@IndividualBased", SqlDbType.Bit) { Value = programa.IndividualBased },
                        new SqlParameter("@Optional", SqlDbType.Bit) { Value = programa.Optional },
                        new SqlParameter("@Mandatory", SqlDbType.Bit) { Value = programa.Mandatory },
                        new SqlParameter("@MembersCount", SqlDbType.Int) { Value = programa.MembersCount },
                        new SqlParameter("@ProgramName", SqlDbType.NVarChar) { Value = programa.ProgramName },
                        new SqlParameter("@Description", SqlDbType.NVarChar) { Value = programa.Description },
                        new SqlParameter("@Duration", SqlDbType.Time) { Value = programa.Duration },
                        new SqlParameter("@FacultyId", SqlDbType.Int) { Value = DBNull.Value }, // Valor DBNull.Value
                        new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = programa.Specialty.SpecialtyId }
                  };
            }
            /*{
              "faceToFace": true,
              "virtual": true,
              "groupBased": true,
              "individualBased": true,
              "optional": true,
              "mandatory": true,
              "membersCount": 0,
              "programName": "Programa575",
              "description": "frrfrfr", 
              "faculty": null,
              "duration": "00:00:00",
              "specialty":{
                "specialtyId": 1
              }
            }*/

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_PROGRAMA_DE_TUTORIA, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el programa de tutoría: " + ex.Message);
            }
        }
        public async Task CrearEditarProgramaDeTutoria(TutoringProgram programa)
        {
            SqlParameter[] parameters;

            DataTable dtTutores = new DataTable();
            dtTutores.Columns.Add("Id",typeof(int));
            foreach( Tutor tutor in programa.Tutors)
            {
                dtTutores.Rows.Add(tutor.TutorId);
            }

            parameters = new SqlParameter[]
                {
                    new SqlParameter("@TutoringProgramID",SqlDbType.Int){ Value= (programa.TutoringProgramId)},
                    new SqlParameter("@FaceToFace", SqlDbType.Bit) { Value = programa.FaceToFace },
                    new SqlParameter("@Virtual", SqlDbType.Bit) { Value = programa.Virtual },
                    new SqlParameter("@GroupBased", SqlDbType.Bit) { Value = programa.GroupBased },
                    new SqlParameter("@IndividualBased", SqlDbType.Bit) { Value = programa.IndividualBased },
                    new SqlParameter("@Optional", SqlDbType.Bit) { Value = programa.Optional },
                    new SqlParameter("@Mandatory", SqlDbType.Bit) { Value = programa.Mandatory },
                    new SqlParameter("@MembersCount", SqlDbType.Int) { Value = programa.MembersCount },
                    new SqlParameter("@ProgramName", SqlDbType.NVarChar) { Value = programa.ProgramName },
                    new SqlParameter("@Description", SqlDbType.NVarChar) { Value = programa.Description },
                    new SqlParameter("@Duration", SqlDbType.Time) { Value = programa.Duration },
                    new SqlParameter("@FacultyId", SqlDbType.Int) { Value = programa.Faculty.FacultyId },
                    new SqlParameter("@SpecialtyId", SqlDbType.Int) { Value = programa.Specialty.SpecialtyId },
                    new SqlParameter("@isActive", SqlDbType.Bit) { Value = programa.IsActive },
                    new SqlParameter("@TutorTypeID", SqlDbType.Int) { Value = programa.TutorTypeId },
                    new SqlParameter("@TutorIdList", SqlDbType.Structured) {Value= dtTutores}

                };
            
            /*{
              "faceToFace": true,
              "virtual": true,
              "groupBased": true,
              "individualBased": true,
              "optional": true,
              "mandatory": true,
              "membersCount": 0,
              "programName": "Programa575",
              "description": "frrfrfr", 
              "faculty": null,
              "duration": "00:00:00",
              "specialty":{
                "specialtyId": 1
              }
            }*/

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_PROGRAMA_DE_TUTORIA, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el programa de tutoría: " + ex.Message);
            }
        }
        public async Task<List<TutoringProgram>> ListarProgramasDeTutoria()
        {
            List<TutoringProgram> programas = new List<TutoringProgram>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMA_DE_TUTORIA, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {

                        TimeSpan duration = (TimeSpan)row["Duration"];

                        Specialty speciality = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                            Name = row["SpecialtyName"].ToString()
                        };

                        Faculty faculty = new Faculty
                        {
                            FacultyId = Convert.ToInt32(row["FacultyId"]),
                            Name = row["FacultyName"].ToString()
                        };
                        TutoringProgram programa = new TutoringProgram
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            FaceToFace = Convert.ToBoolean(row["FaceToFace"]),
                            Virtual = Convert.ToBoolean(row["Virtual"]),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            IndividualBased = Convert.ToBoolean(row["IndividualBased"]),
                            Optional = Convert.ToBoolean(row["Optional"]),
                            Mandatory = Convert.ToBoolean(row["Mandatory"]),
                            MembersCount = Convert.ToInt32(row["MembersCount"]),
                            ProgramName = row["ProgramName"].ToString(),
                            Description = row["Description"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            Duration = (TimeSpan)row["Duration"],
                            StudentsNumber = Convert.ToInt32(row["CantAlumnos"]),
                            TutorsNumber = Convert.ToInt32(row["CantTutores"]),
                            TutorTypeId = Convert.ToInt32(row["TutorTypeId"]),
                            TutorTypeDescription = row["TutorTypeDescription"].ToString(),
                            Faculty = faculty,
                            Specialty = speciality
                    };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría: " + ex.Message);
            }

            return programas;
        }
        public async Task<List<ListarTutoringProgram>> ListarProgramasDeTutoriaPorTutor(int tutorId)
        {
            List<ListarTutoringProgram> programas = new List<ListarTutoringProgram>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                        Value = tutorId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMA_POR_TUTOR, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ListarTutoringProgram programa = new ListarTutoringProgram
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            Description = row["Description"].ToString(),
                            FacultyName = row["FacultyName"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString(),
                            tutorType = row["TutorType"].ToString()
                        };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría por tutor: " + ex.Message);
            }

            return programas;
        }

        public async Task<List<TutoringProgram>> ListarProgramasDeTutoriaPorTipoUsuario(int userAccountTypeId)
        {
            List<TutoringProgram> programas = new List<TutoringProgram>();

            try
            {
                // Crear el parámetro para el UserAccountTypeId
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserAccountTypeId", SqlDbType.Int)
            {
                Value = userAccountTypeId
            }
                };

                // Ejecutar el procedimiento almacenado con el array de parámetros
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTORINGPROGRAM_LISTARXTIPOUSUARIO_SELECT", parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Mapear los datos de la fila a un objeto TutoringProgram
                        TutoringProgram programa = new TutoringProgram
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            FaceToFace = Convert.ToBoolean(row["FaceToFace"]),
                            Virtual = Convert.ToBoolean(row["Virtual"]),
                            GroupBased = Convert.ToBoolean(row["GroupBased"]),
                            IndividualBased = Convert.ToBoolean(row["IndividualBased"]),
                            Optional = Convert.ToBoolean(row["Optional"]),
                            Mandatory = Convert.ToBoolean(row["Mandatory"]),
                            MembersCount = Convert.ToInt32(row["MembersCount"]),
                            ProgramName = row["ProgramName"].ToString(),
                            Description = row["Description"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            Duration = (TimeSpan)row["Duration"]
                        };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría por tipo de usuario: " + ex.Message);
            }

            return programas;
        }

        public async Task<List<TutoringProgramAlumno>> ListarProgramasDeTutoriaPorAlumno(int studentId)
        {
            List<TutoringProgramAlumno> programas = new List<TutoringProgramAlumno>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StudentId", SqlDbType.Int){
                        Value = studentId
                    }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMA_POR_ALUMNO, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutoringProgramAlumno programa = new TutoringProgramAlumno
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            ProgramDescription = row["ProgramDescription"].ToString(),
                            FaceToFace = Convert.ToBoolean(row["FaceToFace"]),
                            Virtual = Convert.ToBoolean(row["Virtual"]),
                            FacultyName = row["FacultyName"].ToString(),
                            SpecialtyName = row["SpecialtyName"].ToString(),
                            TutorType = row["TutorType"].ToString(),
                        };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría por alumno: " + ex.Message);
            }

            return programas;
        }
        public async Task EliminarProgramaTutoria(int tutoringProgramId)
        {
            List<TutoringProgramAlumno> programas = new List<TutoringProgramAlumno>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int){
                        Value = tutoringProgramId
                    }
                };

                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_PROGRAMATUTORIA, parameters);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el programa de tutoria: " + ex.Message);
            }

            return ;
        }

        public async Task<List<TutorProgramaDeTutoria>> ListarProgramasDeTutoriaPorTutorId(int tutorId)
        {
            List<TutorProgramaDeTutoria> programas = new List<TutorProgramaDeTutoria>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@TutorId", SqlDbType.Int){
                Value = tutorId
            }
        };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMAS_POR_TUTOR, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorProgramaDeTutoria programa = new TutorProgramaDeTutoria
                        {
                            TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            ProgramDescription = row["Description"].ToString(),
                            TutorName = row["TutorName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            NameFaculty = row["NameFaculty"].ToString()
                        };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría por tutor: " + ex.Message);
            }

            return programas;
        }

        public async Task<List<StudentProgramaDeTutoria>> ListarProgramasDeTutoriaPorStudentId(int studentId)
        {
            List<StudentProgramaDeTutoria> programas = new List<StudentProgramaDeTutoria>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StudentId", SqlDbType.Int){
                    Value = studentId
                }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMAS_POR_ALUMNO, parameters);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        StudentProgramaDeTutoria programa = new StudentProgramaDeTutoria
                        {
                            StudentProgramId = Convert.ToInt32(row["StudentProgramId"]),
                            ProgramName = row["ProgramName"].ToString(),
                            ProgramDescription = row["Description"].ToString(),
                            StudentName = row["StudentName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            SecondLastName = row["SecondLastName"].ToString(),
                            NameFaculty = row["NameFaculty"].ToString()
                        };

                        programas.Add(programa);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los programas de tutoría por tutor: " + ex.Message);
            }

            return programas;
        }

    }
}
