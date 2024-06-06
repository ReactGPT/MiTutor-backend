namespace MiTutor.Models.TutoringManagement
{
    public class AvailabilityTutor
    {
        public int AvailabilityTutorId { get; set; }

        public DateOnly AvailabilityDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public bool IsActive { get; set; }

        public Tutor Tutor { get; set; }
    }

    public class ListAvailabilityTutor
    {
        public int AvailabilityTutorId { get; set; }
        public DateOnly AvailabilityDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateAvailabilityTutor
    {
        public int AvailabilityTutorId { get; set; }
        public string AvailabilityDate { get; set; } 
        public string StartTime { get; set; } 
        public string EndTime { get; set; } 
        public int TutorId { get; set; }
    }
}
