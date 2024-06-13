using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services
{
    public class NotificacionService
    {
        private readonly DatabaseManager _databaseManager;

        public NotificacionService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task<List<Notificacion>> ListarNotificacionesPorUserAcount(int UserAcountId) {

            List<Notificacion> notificaciones = new List<Notificacion>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserAcountId", SqlDbType.Int) { Value = UserAcountId}
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_NOTIFICACIONES_POR_USUARIO, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Notificacion notificacion = new Notificacion
                        {
                            resumen = row[0].ToString(),
                            descripcion = row[1].ToString(),
                            tipo = row[2].ToString()

                        };
                        notificaciones.Add(notificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarNotificacionesPorUserAcount", ex);
            }


            return notificaciones;
        }

    }
}
