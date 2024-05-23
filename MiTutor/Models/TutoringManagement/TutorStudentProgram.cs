using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class TutorStudentProgram
    {
        public int TutorStudentProgramId { get; set; }

        public string State { get; set; }

        public int IsActive { get; set; }

        public string Motivo { get; set; }

        public int TutorId { get; set; }
        public int StudentProgramId { get; set; }

        //public StudentProgram StudentProgram { get; set; }

        //public Tutor Tutor { get; set; }
    }

    public class TutorStudentProgramModificado
    {
        public int StudentId { get; set; }
        public int ProgramId { get; set; }

        public TutorStudentProgram TutorStudentProgram { get; set; }

    }

}
