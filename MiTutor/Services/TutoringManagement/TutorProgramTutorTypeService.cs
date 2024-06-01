using MiTutor.DataAccess;
using MiTutor.Models.UniversityUnitManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.TutoringManagement;

namespace MiTutor.Services.TutoringManagement
{
    public class TutorProgramTutorTypeService
    {
        private readonly DatabaseManager _databaseManager;

        public TutorProgramTutorTypeService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearTutorProgramTutorType(TutorProgramTutorType tutorProgramTutorType)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TutorId", SqlDbType.Int) { Value = tutorProgramTutorType.Tutor.TutorId },
                    new SqlParameter("@TutoringProgramId", SqlDbType.Int) { Value = tutorProgramTutorType.TutoringProgram.TutoringProgramId },
                    new SqlParameter("@TutorTypeId", SqlDbType.Int) { Value = tutorProgramTutorType.TutorType.TutorTypeId } 
                };

                await _databaseManager.ExecuteStoredProcedure("TUTOR_PROGRAM_TYPE_INSERTAR_INSERT", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el tutor program tutor type: " + ex.Message);
            }
        }

        public async Task<List<TutorProgramTutorType>> ListarTutorProgramTutorType()
        {
            List<TutorProgramTutorType> tutorProgramTutorTypes = new List<TutorProgramTutorType>();

            try
            {  
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_PROGRAMA_TUTOR_TIPO_TUTOR, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TutorProgramTutorType tutorProgramTutorType = new TutorProgramTutorType
                        {
                            TutorProgramTutorTypeId = Convert.ToInt32(row["TutorProgramTutorTypeId"]),
                            Tutor = new Tutor
                            {
                                TutorId = Convert.ToInt32(row["TutorId"])
                            },
                            TutoringProgram = new TutoringProgram
                            {
                                TutoringProgramId = Convert.ToInt32(row["TutoringProgramId"])
                            },
                            TutorType = new TutorType
                            {
                                TutorTypeId = Convert.ToInt32(row["TutorTypeId"])
                            }, 
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        tutorProgramTutorTypes.Add(tutorProgramTutorType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los tutor program tutor types: " + ex.Message);
            }

            return tutorProgramTutorTypes;
        }


    }
}
