using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.TutoringManagement;

namespace MiTutor.Models.UniversityUnitManagement
{
    public class Faculty
    {
        public int FacultyId { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; } 

        public int NumberOfStudents { get; set; }

        public int NumberOfTutors { get; set; }

        public bool IsActive { get; set; }

        public UserAccount FacultyManager { get; set; }

        public ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();

        public ICollection<TutoringProgram> TutoringPrograms { get; set; } = new List<TutoringProgram>();

    }
}
