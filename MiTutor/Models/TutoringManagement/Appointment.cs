using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateOnly CreationDate { get; set; }

        public string Reason { get; set; }

        public bool IsActive { get; set; }

        public bool IsInPerson { get; set; }

        public string Classroom { get; set; }

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
        public Appointment appointment { get; set; }
        public int IdProgramTutoring { get; set; }
        public int IdTutor { get; set; }
        public int[] IdStudent { get; set; }
    }

    public class ListarAppointment
    {
        public int AppointmentId { get; set; }
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

    }
}
