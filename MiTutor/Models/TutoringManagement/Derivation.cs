using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Models.TutoringManagement
{
    public class Derivation
    {
        public int DerivationId { get; set; }

        public string Reason { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public DateOnly CreationDate { get; set; }  
 
        public bool IsActive { get; set; }

        public Appointment Appointment { get; set; }

        public UnitDerivation UnitDerivation { get; set; }

        public UserAccount UserAccountDo { get; set; }

    }
}
