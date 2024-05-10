using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Models.TutoringManagement
{
    public class TutoringProgram
    {
        public int TutoringProgramId { get; set; }

        public bool FaceToFace { get; set; }

        public bool Virtual { get; set; }

        public bool GroupBased { get; set; }

        public bool IndividualBased { get; set; }

        public bool Optional { get; set; }

        public bool Mandatory { get; set; }

        public int  MembersCount { get; set; }

        public string ProgramName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public TimeSpan Duration { get; set; } 
        
        public Faculty Faculty { get; set; }

        public Specialty Specialty { get; set; }

    }

    public class ListarTutoringProgram
    {
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public string FacultyName { get; set; }
        public string SpecialtyName { get; set; }
        public string tutorType { get; set; }
    }
}
