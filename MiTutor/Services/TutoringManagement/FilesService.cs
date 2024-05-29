using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.GestionUsuarios;

namespace MiTutor.Services.TutoringManagement
{
    public class FilesService
    {
        private readonly DatabaseManager _databaseManager;
        private readonly CommentService _commentService;

        public FilesService()
        {
            _databaseManager = new DatabaseManager();
            _commentService = new CommentService();
        }

        public async Task<int> InsertarArchivo(Files archivo)
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FilesName", SqlDbType.VarChar, 255) { Value = archivo.FilesName },
                    new SqlParameter("@AppointmentResultId", SqlDbType.Int) { Value = archivo.AppointmentResultId },
                    new SqlParameter("@PrivacyTypeId", SqlDbType.Int) { Value = archivo.PrivacyTypeId },
                    new SqlParameter("@FileId", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                // Llamar al procedimiento almacenado
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_ARCHIVO, parameters);

                archivo.FilesId = Convert.ToInt32(parameters[parameters.Length - 1].Value);
                return archivo.FilesId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el archivo", ex);
            }
        }

        public async Task<List<FileBD>> ListarArchivosPorIdResultadoTipo(int idResultado, int idTipo)
        {
            List<FileBD> archivos = new List<FileBD>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@AppointmentResultId", SqlDbType.Int) { Value = idResultado },
                    new SqlParameter("@PrivacyTypeId", SqlDbType.Int) { Value = idTipo }
                };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable("FILES_LISTAR_POR_ID_RESULTADOCITA_TIPOPRIVACIDAD_SELECT", parameters);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        FileBD archivo = new FileBD()
                        {
                            FilesId = Convert.ToInt32(row["FilesId"]),
                            FilesName = row["FilesName"].ToString(),
                            Activo = Convert.ToBoolean(row["IsActive"])
                    };

                        archivos.Add(archivo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarArchivosPorIdResultadoTipo", ex);
            }

            return archivos;
        }

        public async Task EliminarArchivo(int filesId)
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = new SqlParameter[]
                { 
                    new SqlParameter("@FilesId", SqlDbType.Int) { Value= filesId } 
                };

                // Llamar al procedimiento almacenado
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_ARCHIVO, parameters);
                 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el archivo", ex);
            }
             
        }

        public async Task ReactivarArchivo(int filesId)
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@FilesId", SqlDbType.Int) { Value= filesId }
                };

                // Llamar al procedimiento almacenado
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.REACTIVAR_ARCHIVO, parameters);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el archivo", ex);
            }

        }

    }
}
