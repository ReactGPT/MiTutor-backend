using MiTutor.DataAccess;
using MiTutor.Models.UniversityUnitManagement;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services.UniversityUnitManagement
{
    public class UnitDerivationService
    {
        private readonly DatabaseManager _databaseManager;

        public UnitDerivationService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task CrearUnidadDerivacion(UnitDerivation unidad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = unidad.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = unidad.Acronym },
                new SqlParameter("@Responsible", SqlDbType.NVarChar) { Value = unidad.Responsible },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = unidad.Email },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = unidad.Phone }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_UNIDAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la unidad: " + ex.Message);
            }
        }

        public async Task<List<UnitDerivation>> ListarUnidades()
        {
            List<UnitDerivation> unidades = new List<UnitDerivation>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_UNIDADES, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UnitDerivation unidad = new UnitDerivation();

                        unidad.UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]);
                        unidad.Name = row["Name"].ToString();
                        unidad.Acronym = row["Acronym"].ToString();
                        unidad.Responsible = row["Responsible"].ToString();
                        unidad.IsActive = Convert.ToBoolean(row["IsActive"]);
                        unidad.Email = row["Email"].ToString();
                        unidad.Phone = row["Phone"].ToString();
                        unidad.CreationDate = Convert.ToDateTime(row["CreationDate"]);
                        unidad.IsParent = Convert.ToBoolean(row["IsParent"]);
                        unidades.Add(unidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUnidades", ex);
            }

            return unidades;
        }

        public async Task<List<UnitDerivation>> ListarSubUnidadesPorUnidad(int unidadId)
        {
            List<UnitDerivation> unidades = new List<UnitDerivation>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = unidadId },
            };

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_SUB_UNIDADES, parameters);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UnitDerivation unidad = new UnitDerivation();

                        unidad.UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]);
                        unidad.Name = row["Name"].ToString();
                        unidad.Acronym = row["Acronym"].ToString();
                        unidad.Responsible = row["Responsible"].ToString();
                        unidad.IsActive = Convert.ToBoolean(row["IsActive"]);
                        unidad.Email = row["Email"].ToString();
                        unidad.Phone = row["Phone"].ToString();
                        unidad.CreationDate = Convert.ToDateTime(row["CreationDate"]);
                        unidad.IsParent = Convert.ToBoolean(row["IsParent"]);
                        unidades.Add(unidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUnidades", ex);
            }

            return unidades;
        }

        public async Task ActualizarUnidadDerivacion(UnitDerivation unidad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UnitDerivationId", SqlDbType.NVarChar) { Value = unidad.UnitDerivationId },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = unidad.Name },
                new SqlParameter("@Acronym", SqlDbType.NVarChar) { Value = unidad.Acronym },
                new SqlParameter("@Responsible", SqlDbType.NVarChar) { Value = unidad.Responsible },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = unidad.Email },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = unidad.Phone }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_UNIDAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ActualizarUnidadDerivacionService", ex);
            }
        }

        public async Task EliminarUnidadDerivacion(int unidadId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = unidadId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_UNIDAD, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en EliminarUnidadDerivacion Service", ex);
            }
        }

    }
}
