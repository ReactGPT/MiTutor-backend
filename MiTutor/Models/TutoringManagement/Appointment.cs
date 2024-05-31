using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Appointment
    {
        [JsonIgnore]
        public int AppointmentId { get; set; }

        public String StartTime { get; set; }

        public String EndTime { get; set; }

        public String CreationDate { get; set; }

        public string Reason { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; }

        public bool IsInPerson { get; set; }

        public string Classroom { get; set; }
        [JsonIgnore]
        public AppointmentStatus AppointmentStatus { get; set; }
        [JsonIgnore]
        public Derivation Derivation { get; set; }
        [JsonIgnore]
        public StudentProgram StudentProgram { get; set; }
        [JsonIgnore]
        public Tutor Tutor { get; set; }
    }

    public class RegisterAppointment
    {
        public Appointment Appointment { get; set; }
        public int IdProgramTutoring { get; set; }
        public int IdTutor { get; set; }
        public int[] IdStudent { get; set; }
    }

    public class ListarAppointment
    {
        public int AppointmentId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string AppointmentStatus { get; set; }
        public bool GroupBased { get; set; }
        public DateOnly CreationDate { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public bool IsInPerson { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Reason { get; set; }

        public int TutorId { get; set; }
        public string TutorName { get; set; }
        public string TutorLastName { get; set; }
        public string TutorSecondLastName { get; set; }
        public string TutorEmail { get; set; }
        public string TutorMeetingRoom { get; set; }
        /*public int CommentId { get; set; }
        public string CommentMessage { get; set; }*/
    }
}
