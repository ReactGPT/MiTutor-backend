using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutor.Models.GestionUsuarios;
using System;

namespace MiTutor.Services.TutoringManagement
{
    public class TutorStudentProgramService
    {
        private readonly DatabaseManager _databaseManager;

        public TutorStudentProgramService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearTutorStudentProgram(TutorStudentProgramModificado tutorStudentProgramModificado)
        {
            await GetStudentProgramIdAsync(tutorStudentProgramModificado);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentProgramId", SqlDbType.Int) { Value = tutorStudentProgramModificado.TutorStudentProgram.StudentProgramId },
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = tutorStudentProgramModificado.TutorStudentProgram.TutorId },
                new SqlParameter("@Motivo", SqlDbType.NVarChar) { Value = tutorStudentProgramModificado.TutorStudentProgram.Motivo}
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_TUTOR_STUDENT_PROGRAM, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la relación Tutor-Student-Program: " + ex.Message);
            }
        }

        public async Task GetStudentProgramIdAsync(TutorStudentProgramModificado tutorStudentProgramModificado)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@StudentId", SqlDbType.Int) { Value = tutorStudentProgramModificado.StudentId },
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = tutorStudentProgramModificado.ProgramId },
                    new SqlParameter("@StudentProgramId", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                await _databaseManager.ExecuteStoredProcedure("GetStudentProgramId", parameters);

                tutorStudentProgramModificado.TutorStudentProgram.StudentProgramId = Convert.ToInt32(parameters[parameters.Length - 1].Value);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el StudentProgramId: " + ex.Message);
            }
        }

        public async Task<List<Solicitud>> ListarSolicitudesPorFacultad(int facultyId)
        {
            List<Solicitud> solicitudes = new List<Solicitud>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = facultyId }
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_STUDENT_PROGRAM_LISTARXFACULTAD_SELECT", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    Solicitud solicitud = new Solicitud
                    {
                        Codigo = Convert.ToInt32(row["Codigo"]),
                        Nombres = row["Nombres"].ToString(),
                        Especialidad = row["Especialidad"].ToString(),
                        Tutor = row["Tutor"].ToString(),
                        FechaSolicitud = Convert.ToDateTime(row["FechaSolicitud"]),
                        Estado = row["Estado"].ToString()
                    };
                    solicitudes.Add(solicitud);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las solicitudes: " + ex.Message);
            }

            return solicitudes;
        }

        public async Task<List<TutorStudentProgram>> ListarTutorStudentProgram(string tutorFirstName, string tutorLastName, string state, int? tutoringProgramId)
        {
            List<TutorStudentProgram> tutorStudentPrograms = new List<TutorStudentProgram>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TutorFirstName", SqlDbType.NVarChar) { Value = (object)tutorFirstName ?? DBNull.Value },
                new SqlParameter("@TutorLastName", SqlDbType.NVarChar) { Value = (object)tutorLastName ?? DBNull.Value },
                new SqlParameter("@State", SqlDbType.NVarChar) { Value = (object)state ?? DBNull.Value },
                new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = (object)tutoringProgramId ?? DBNull.Value }
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("TUTOR_STUDENT_PROGRAM_LISTAR_X_FILTROS", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    TutorStudentProgram tutorStudentProgram = new TutorStudentProgram
                    {
                        TutorStudentProgramId = Convert.ToInt32(row["TutorStudentProgramId"]),
                        StudentId = Convert.ToInt32(row["StudentId"]),
                        StudentFirstName = row["StudentFirstName"].ToString(),
                        StudentLastName = row["StudentLastName"].ToString(),
                        StudentCode = row["StudentCode"].ToString(),
                        SpecialtyId = Convert.ToInt32(row["SpecialtyId"]),
                        SpecialtyName = row["SpecialtyName"].ToString(),
                        SpecialtyAcronym = row["SpecialtyAcronym"].ToString(),
                        TutorId = Convert.ToInt32(row["TutorId"]),
                        TutorFirstName = row["TutorFirstName"].ToString(),
                        TutorLastName = row["TutorLastName"].ToString(),
                        ProgramName = row["ProgramName"].ToString(),
                        ProgramDescription = row["ProgramDescription"].ToString(),
                        RequestDate = Convert.ToDateTime(row["RequestDate"]).ToString("yyyy-MM-dd"),
                        State = row["State"].ToString(),
                        IsActive = Convert.ToInt32(row["IsActive"]),
                        Motivo = row["Motivo"].ToString(),
                        StudentProgramId = Convert.ToInt32(row["StudentProgramId"])
                    };
                    tutorStudentPrograms.Add(tutorStudentProgram);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los TutorStudentProgram: " + ex.Message);
            }

            return tutorStudentPrograms;
        }
        
        public async Task ActualizarEstadoTutorStudentProgram(string tutorStudentProgramIds, string newState)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TutorStudentProgramIds", SqlDbType.NVarChar) { Value = tutorStudentProgramIds },
                new SqlParameter("@NewState", SqlDbType.NVarChar) { Value = newState }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure("TUTOR_STUDENT_PROGRAM_UPDATE_ESTADO", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado: " + ex.Message);
            }
        }
    }
}
