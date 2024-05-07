namespace MiTutor.Models.TutoringManagement
{
    public class TutorProgramTutorType
    {
        public int TutorProgramTutorTypeId { get; set; } 

        public bool IsActive { get; set; }

        public Tutor Tutor { get; set; }

        public TutorType TutorType { get; set; }

        public TutoringProgram TutoringProgram { get; set; }

    }
}
