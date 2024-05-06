using MiTutor.DataAccess;
using MiTutor.Models;
using System.Data.SqlClient;
using System.Data;

namespace MiTutor.Services
{
    public class EspecialidadService
    { 
        private readonly DatabaseManager _databaseManager;

        /*
        public EspecialidadService()
        {
            _databaseManager = new DatabaseManager();
        }

        public async Task CrearEspecialidad(Especialidad especialidad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@nombre", SqlDbType.VarChar) { Value = especialidad.Nombre },
                //new SqlParameter("@id", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_ESPECIALIDAD, parameters);
            }
            catch
            {
                throw new Exception("ERROR en CrearEspecialidadService");
            }
        }

        public async Task<List<Especialidad>> ListarEspecialidades()
        {
            List<Especialidad> especialidades = new List<Especialidad>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_ESPECIALIDAD, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Especialidad especialidad = new Especialidad
                        {
                            Id = Convert.ToInt32(row[0]),
                            Nombre = row[1].ToString()
                        };
                        especialidades.Add(especialidad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarEspecialidades", ex);
            }

            return especialidades;
        }

        public async Task ActualizarEspecialidad(Especialidad especialidad)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = especialidad.Id },
                new SqlParameter("@nombre", SqlDbType.VarChar) { Value = especialidad.Nombre }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_ESPECIALIDAD, parameters);
            }
            catch
            {
                throw new Exception("ERROR en ActualizarEspecialidadService");
            }
        }
        */



    }
}
