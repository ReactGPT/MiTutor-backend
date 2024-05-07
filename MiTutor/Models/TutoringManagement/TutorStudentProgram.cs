namespace MiTutor.Models.TutoringManagement
{
    public class TutorStudentProgram
    {
        public int TutorStudentProgramId { get; set; } 

        public string State { get; set; }

        public int IsActive { get; set; }

        public StudentProgram StudentProgram { get; set; }

        public Tutor Tutor { get; set; }
    }
}
