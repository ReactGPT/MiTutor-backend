using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.TutoringManagement;

namespace MiTutor.Models.UniversityUnitManagement
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int NumberOfStudents { get; set; }  
        public Faculty Faculty { get; set; }
        public bool IsActive { get; set; } 
        public UserAccount SpecialtyManager { get; set; }
        public UserAccount? PersonalApoyo { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<TutoringProgram> TutoringPrograms { get; set; } = new List<TutoringProgram>();

    }
}
