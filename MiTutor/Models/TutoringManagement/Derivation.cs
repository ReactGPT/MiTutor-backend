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
}