namespace MiTutor.Models.UniversityUnitManagement
{
    public class UnitDerivation
    {
        public int UnitDerivationId { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public string Responsible { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        //public virtual ICollection<Derivation> Derivations { get; set; } = new List<Derivation>();

        //public virtual ICollection<UnitSubUnit> UnitSubUnitSubUnitDerivations { get; set; } = new List<UnitSubUnit>();

    }
}
