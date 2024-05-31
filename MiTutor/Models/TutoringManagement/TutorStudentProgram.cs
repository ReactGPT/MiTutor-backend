using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class TutorStudentProgram
    {
        public int TutorStudentProgramId { get; set; }
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentCode { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string SpecialtyAcronym { get; set; }
        public int TutorId { get; set; }
        public string TutorFirstName { get; set; }
        public string TutorLastName { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public string RequestDate { get; set; }
        public string State { get; set; }
        public int IsActive { get; set; }
        public string Motivo { get; set; }
        public int StudentProgramId { get; set; }
    }

    public class TutorStudentProgramModificado
    {
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
        public TutorStudentProgram TutorStudentProgram { get; set; }
    }
}
