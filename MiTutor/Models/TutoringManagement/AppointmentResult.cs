using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace MiTutor.Models.TutoringManagement
{
    public class AppointmentResult
    {
        public int AppointmentResultId { get; set; }

        public bool Asistio {  get; set; }

        public bool IsActive { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        [JsonIgnore]
        public  ICollection<Files> Files { get; set; } = new List<Files>();
         
        //tienes el id de cita, alumno y el programa
    }
    public class InsertAppointmentResult
    {
        public AppointmentResult appointmentResult { get; set; } = new AppointmentResult();
        public int studentId { get; set; }
        public int tutoringProgramId { get; set; }
        public int appointmentId { get; set; }

    }
}
