using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services
{
	public class NotificationService
	{
		private readonly DatabaseManager _databaseManager;

        public NotificationService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }
		
		public async void CreateTutorCancellationNotification(int tutorId, string title)
		{
			SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TutorId", SqlDbType.Int) { Value = tutorId },
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = title }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_NOTIFICAR_CANCELACION_TUTOR, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CreateTutorCancellationNotification");
            }
		}

		public async void CreateStudentCancellationNotification(List<int> studentIds, string title)
		{
            string studentIdsString = string.Join(",", studentIds);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@StudentIds", SqlDbType.NVarChar) { Value = studentIdsString },
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = title }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_NOTIFICAR_CANCELACION_ALUMNOS, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CreateStudentCancellationNotification");
            }
		}
	}
}