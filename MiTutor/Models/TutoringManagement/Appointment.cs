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

        public Derivation Derivation{ get; set; }

        public StudentProgram StudentProgram { get; set; }

        public Tutor Tutor { get; set; }
    }
}
