using MiTutor.Models.GestionUsuarios;
using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Tutor
    {
        public int TutorId { get; set; }

        public string MeetingRoom { get; set; }

        public bool IsActive { get; set; } 

        public UserAccount UserAccount { get; set; }
        [JsonIgnore]
        public ICollection<AvailabilityTutor> AvailabilityTutors { get; set; } = new List<AvailabilityTutor>();
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
