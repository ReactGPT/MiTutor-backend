using MiTutor.Models.GestionUsuarios;

namespace MiTutor.Models.TutoringManagement
{
    public class Tutor
    {
        public int TutorId { get; set; }

        public string MeetingRoom { get; set; }

        public bool IsActive { get; set; } 

        public UserAccount UserAccount { get; set; } 

        public ICollection<AvailabilityTutor> AvailabilityTutors { get; set; } = new List<AvailabilityTutor>();
        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
