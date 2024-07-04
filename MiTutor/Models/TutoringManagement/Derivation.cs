using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.UniversityUnitManagement;
using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Derivation
    {
        public int DerivationId { get; set; }

        public string Reason { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public DateOnly CreationDate { get; set; }

        public int UnitDerivationId { get; set; }

        //public int TutorId { get; set; }

        public int AppointmentId { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public Appointment Appointment { get; set; }

        [JsonIgnore]
        public UnitDerivation UnitDerivation { get; set; }

        [JsonIgnore]
        public UserAccount UserAccountDo { get; set; }

        public int FacultyId { get; set; }

    }

    public class ListDerivation
    {
        public int DerivationId { get; set; }
        public string Reason { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public DateOnly CreationDate { get; set; }

        public string UnitDerivationName { get; set; }

        public string NombreAlumno { get; set; }

        public string Codigo { get; set; }
        public string ProgramName { get; set; }
    }

    public class DerivationBienestar
    {
        public int DerivationId { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateOnly CreationDate { get; set; }
        public string UnitDerivationName { get; set; }
        public int IdUsuarioAlumno { get; set; }
        public string CorreoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdTutor { get; set; }
        public int IdUsuarioTutor { get; set; }
        public string CorreoTutor { get; set; }
        public string NombreTutor { get; set; }
        public string CodigoTutor { get; set; }
        public string ProgramName { get; set; }
        public string Observaciones { get; set; }
    }
}