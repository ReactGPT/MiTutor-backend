using MiTutor.DataAccess;
using MiTutor.Models.TutoringManagement;
using System.Data.SqlClient;
using System.Data;
using MiTutor.Models.UniversityUnitManagement;
using System.Xml.Linq;
using MiTutor.Models.GestionUsuarios;

namespace MiTutor.Services.TutoringManagement
{
    public class DerivationService
    {
        private readonly DatabaseManager _databaseManager;

        public DerivationService(DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager ?? throw new ArgumentNullException(nameof(databaseManager));
        }

        public async Task<int> CrearDerivacion(Derivation derivation)
        {
            // Convertir DateOnly a DateTime
            DateTime creationDate = DateTime.Parse(derivation.CreationDate.ToString());

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Reason", SqlDbType.NVarChar) { Value = derivation.Reason },
                new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = derivation.Comment },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = derivation.Status },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = creationDate },
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = derivation.UnitDerivationId },
                //new SqlParameter("@TutorId", SqlDbType.Int) { Value = derivation.TutorId },
                new SqlParameter("@AppointmentId", SqlDbType.Int) { Value = derivation.AppointmentId },
                new SqlParameter("@DerivationId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@FacultyId", SqlDbType.Int) { Value = derivation.FacultyId }
                //FacultyId
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.CREAR_DERIVACION, parameters);
                // Obtener el ID del resultado recién insertado
                return Convert.ToInt32(parameters[parameters.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en CrearDerivacion", ex);
            }
        }

        public async Task<List<Derivation>> ListarDerivations()
        {
            List<Derivation> derivations = new List<Derivation>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_DERIVACIONES, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime dateTime = Convert.ToDateTime(row["CreationDate"]);
                        DateOnly creationDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day); // Crear DateOnly a partir de DateTime
                        Derivation derivation = new Derivation
                        {
                            DerivationId = Convert.ToInt32(row["DerivationId"]),
                            Reason = row["Reason"].ToString(),
                            Comment = row["Comment"].ToString(),
                            Status = row["Status"].ToString(),
                            CreationDate = creationDate,
                            UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]),
                            //TutorId = Convert.ToInt32(row["TutorId"]),
                            AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                            IsActive = Convert.ToBoolean(row["IsActive"])
                        };
                        derivations.Add(derivation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarDerivaciones", ex);
            }

            return derivations;
        }
        public async Task ActualizarDerivacion(Derivation derivation)
        {
            // Convertir DateOnly a DateTime
            DateTime creationDate = DateTime.Parse(derivation.CreationDate.ToString());
            SqlParameter[] parameters = new SqlParameter[]
            {

                new SqlParameter("@DerivationId", SqlDbType.Int) { Value = derivation.DerivationId },
                new SqlParameter("@Reason", SqlDbType.NVarChar) { Value = derivation.Reason },
                new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = derivation.Comment },
                new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = creationDate },
                new SqlParameter("@UnitDerivationId", SqlDbType.Int) { Value = derivation.UnitDerivationId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ACTUALIZAR_DERIVACION, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ActualizarDerivacionService", ex);
            }
        }

        public async Task EliminarDerivacion(int derivationId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DerivationId", SqlDbType.Int) { Value = derivationId }
            };

            try
            {
                await _databaseManager.ExecuteStoredProcedure(StoredProcedure.ELIMINAR_DERIVACION, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en EliminarDerivacion", ex);
            }
        }

        //Unidades de Derivación
        public async Task<List<UnitDerivation>> ListarUnidadesDerivacion()
        {
            List<UnitDerivation> unitDerivations = new List<UnitDerivation>();

            try
            {
                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.LISTAR_UNIDADES_DERIVACION, null);
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        UnitDerivation derivation = new UnitDerivation
                        {
                            UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]),
                            Name = row["Name"].ToString()
                        };
                        unitDerivations.Add(derivation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en ListarUnidadesDerivacion", ex);
            }

            return unitDerivations;
        }

        public async Task<Derivation> SeleccionarDerivacionByIdAppointment(int appointmentId)
        {
            Derivation derivation = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AppoinmentId", SqlDbType.Int){
                         Value = appointmentId
                    }
            };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.SELECCIONAR_DERIVATION_ID_CITA, parameters);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0]; // Selecciona la primera fila ya que esperas solo un estudiante
                    DateTime dateTime = Convert.ToDateTime(row["CreationDate"]);
                    DateOnly creationDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day); // Crear DateOnly a partir de DateTime
                    derivation = new Derivation()
                    {
                        DerivationId = Convert.ToInt32(row["DerivationId"]),
                        Reason = row["Reason"].ToString(),
                        Comment = row["Comment"].ToString(),
                        Status = row["Status"].ToString(),
                        CreationDate = creationDate,
                        UnitDerivationId = Convert.ToInt32(row["UnitDerivationId"]),
                        //TutorId = Convert.ToInt32(row["TutorId"]),
                        AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    };
                    derivation.AppointmentId = appointmentId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en SeleccionarDatosEstudiantesById", ex);
            }

            return derivation;
        }

        public async Task<List<ListDerivation>> SeleccionarPorTutor(int idTutor)
        {
            List<ListDerivation> derivations = new List<ListDerivation>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TutorId", SqlDbType.Int){
                        Value = idTutor
                    }
            };

                DataTable dataTable = await _databaseManager.ExecuteStoredProcedureDataTable(StoredProcedure.SELECCIONAR_DERIVATION_POR_ID_TUTOR, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime dateTime = Convert.ToDateTime(row["CreationDate"]);
                    DateOnly creationDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

                    ListDerivation derivation = new ListDerivation()
                    {
                        DerivationId = Convert.ToInt32(row["DerivationId"]),
                        Reason = row["Reason"].ToString(),
                        Comment = row["Comment"].ToString(),
                        UnitDerivationName = row["Name"].ToString(),
                        NombreAlumno = row["NombreAlumno"].ToString(),
                        Codigo= row["PUCPCode"].ToString(),
                        Status = row["Status"].ToString(),
                        CreationDate = creationDate,
                        ProgramName = row["ProgramName"].ToString(),

                    };

                    derivations.Add(derivation);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR en SeleccionarPorTutor", ex);
            }

            return derivations;
        }

    }
}
