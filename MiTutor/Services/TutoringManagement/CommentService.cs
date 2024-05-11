using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.TutoringManagement
{
    public class CommentService
    {
        private readonly DatabaseManager _databaseManager;


        public CommentService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearComentario(Comment comment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Message", SqlDbType.NVarChar) { Value = comment.Message },
                new SqlParameter("@AppointmentResultId", SqlDbType.Int) { Value = comment.AppointmentResultId },
                new SqlParameter("@PrivacyTypeId", SqlDbType.Int) { Value = comment.PrivacyTypeId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_COMENTARIO, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearComentarioService");
            }
        }

        public async Task<List<Comment>> ListarComments()
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_COMENTARIOS, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Comment comment = new Comment
                        {
                            CommentId = Convert.ToInt32(row[0]),
                            Message = row[1].ToString(),
                            IsActive = Convert.ToBoolean(row[2]),
                            AppointmentResultId = Convert.ToInt32(row[3]),
                            PrivacyTypeId = Convert.ToInt32(row[4])
                        };
                        comments.Add(comment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarComentarios", ex);
            }

            return comments;
        }

        public async Task ActualizarComentario(Comment comment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CommentId", SqlDbType.Int) { Value = comment.CommentId },
                new SqlParameter("@Message", SqlDbType.NVarChar) { Value = comment.Message },
                new SqlParameter("@AppointmentResultId", SqlDbType.Int) { Value = comment.AppointmentResultId },
                new SqlParameter("@PrivacyTypeId", SqlDbType.Int) { Value = comment.PrivacyTypeId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_COMENTARIO, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarComentarioService");
            }
        }

        public async Task ActualizarComentarioxID(int  id_comment, string message)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id_comment", SqlDbType.Int) { Value = id_comment },
                new SqlParameter("@message", SqlDbType.NVarChar) { Value = message } 
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_COMMENT_X_ID, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarComentarioService");
            }
        }


        public async Task EliminarComentario(int commentId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CommentId", SqlDbType.Int) { Value = commentId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_COMENTARIO, parameters);
            }
            catch
            {
                throw new Exception("ERROR en EliminarComentario");
            }
        }

        public async Task ConsultarComentario_x_ID_ResultadoCita(int idAppointmentResult, List<Comment> comments)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id_appointment_result", SqlDbType.Int) { Value = idAppointmentResult }
                };

                // Ejecutar el procedimiento almacenado para obtener los comentarios filtrados por id_appointmentresult
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.CONSULTAR_COMENTARIOS_X_ID_RESULTADO_CITA, parameters);

                // Verificar si se obtuvieron resultados
                if (dataTable != null)
                {
                    // Recorrer cada fila del resultado
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Crear un nuevo objeto Comment y asignar los valores de la fila
                        Comment comment = new Comment
                        {
                            CommentId = Convert.ToInt32(row["CommentId"]),
                            Message = row["Message"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"]),
                            AppointmentResultId = Convert.ToInt32(row["AppointmentResultId"]),
                            PrivacyTypeId = Convert.ToInt32(row["PrivacyTypeId"])
                        };

                        // Agregar el comentario a la lista
                        comments.Add(comment);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la ejecución del procedimiento almacenado
                throw new Exception("Error al consultar comentarios: " + ex.Message);
            }
        }
    }
}
