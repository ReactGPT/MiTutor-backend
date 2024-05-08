using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class TutorStudentProgram
    {
        public int TutorStudentProgramId { get; set; }

        public string State { get; set; }

        public int IsActive { get; set; }
        public int StudentProgramId { get; set; }
        [JsonIgnore]
        public StudentProgram StudentProgram { get; set; }
        [JsonIgnore]
        public Tutor Tutor { get; set; }
    }
}
