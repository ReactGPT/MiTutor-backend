using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace MiTutor.Models.TutoringManagement
{
    public class AppointmentResult
    {
        public int AppointmentResultId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsActive { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        [JsonIgnore]
        public  ICollection<File> Files { get; set; } = new List<File>();
        [JsonIgnore]
        public Appointment Appointment { get; set; }
    }
}
