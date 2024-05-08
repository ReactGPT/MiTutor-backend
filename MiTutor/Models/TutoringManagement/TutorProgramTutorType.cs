using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class TutorProgramTutorType
    {
        public int TutorProgramTutorTypeId { get; set; }

        public bool IsActive { get; set; }

        public int idTutor { get; set; }

        public int TutoringProgramId { get; set; }
        [JsonIgnore]
        public Tutor Tutor { get; set; }

        public TutorType TutorType { get; set; }
        [JsonIgnore]
        public TutoringProgram TutoringProgram { get; set; }

    }
}
