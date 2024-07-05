using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services
{
    public class InstitutionService
    {
        private readonly DatabaseManager _databaseManager;
        public InstitutionService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }
        public async Task CrearInstitucion(Institution institucion)
        {
            byte[] logoBytes = Convert.FromBase64String(institucion.Logo);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = institucion.Name },
                new SqlParameter("@Address", SqlDbType.VarChar) {Value = institucion.Address},
                new SqlParameter("@District", SqlDbType.VarChar) { Value = institucion.District },
                new SqlParameter("@InstitutionType", SqlDbType.VarChar) { Value = institucion.InstitutionType },
                new SqlParameter("@Logo", SqlDbType.VarBinary) { Value = logoBytes }
            };
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.INSERTAR_INSTITUCION, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearInstitucion");
            }
        }

        public async Task<List<Institution>> ListarInstituciones()
        {
            List<Institution> instituciones = new List<Institution>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_INSTITUCION, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Institution institucion = new Institution();
                        institucion.Id = Convert.ToInt32(row["InstitutionId"]);
                        institucion.Name = row["Name"].ToString();
                        institucion.Address = row["Address"].ToString();
                        institucion.District = row["District"].ToString();
                        institucion.InstitutionType = row["InstitutionType"].ToString();
                        byte[] logoBytes = row["Logo"] as byte[];
                        if (logoBytes != null)
                        {
                            institucion.Logo = Convert.ToBase64String(logoBytes);
                        }
                        institucion.IsActive = Convert.ToBoolean(row["IsActive"]);
                        instituciones.Add(institucion);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarInstituciones", ex);
            }

            return instituciones;
        }

        public async Task ActualizarInstitucion(Institution institucion)
        {
            byte[] logoBytes = string.IsNullOrEmpty(institucion.Logo) ? null : Convert.FromBase64String(institucion.Logo);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = institucion.Id },
                new SqlParameter("@Name", SqlDbType.VarChar) { Value = institucion.Name },
                new SqlParameter("@Address", SqlDbType.VarChar) {Value = institucion.Address},
                new SqlParameter("@District", SqlDbType.VarChar) { Value = institucion.District },
                new SqlParameter("@InstitutionType", SqlDbType.VarChar) { Value = institucion.InstitutionType },
                new SqlParameter("@Logo", SqlDbType.VarBinary) { Value = (object)logoBytes ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = institucion.IsActive }
            };
            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_INSTITUCION, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarInstitucion");
            }
        }
    }
}
