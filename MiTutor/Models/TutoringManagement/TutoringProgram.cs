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
        public int MembersCount { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan Duration { get; set; }
        public Faculty Faculty { get; set; }
        public Specialty Specialty { get; set; }
        public int StudentsNumber { get; set; }
        public int TutorsNumber { get; set; }
        public int TutorTypeId { get; set; }
        public string TutorTypeDescription { get; set; }
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

    public class TutoringProgramAlumno
    {
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public bool FaceToFace { get; set; }
        public bool Virtual { get; set; }
        public string FacultyName { get; set; }
        public string SpecialtyName { get; set; }
        public string TutorType { get; set; }
    }

    //Indicador
    public class TutorProgramaDeTutoria
    {
        public int TutoringProgramId { get; set; }

        public string ProgramName { get; set; } 

        public string ProgramDescription { get; set; }

        public string TutorName {  get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }

        public string NameFaculty {  get; set; }


    }


}
