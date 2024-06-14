using MiTutor.Models.GestionUsuarios;
using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class StudentProgram
    {
        public int StudentProgramId { get; set; } 
          
        public DateOnly JoinDate { get; set; }
        public int idStudent { get; set; }
        public int TutoringProgramId { get; set; }
        public int TutoringId { get; set; }
        public bool IsActive { get; set; }
        public Student Student { get; set; }
        [JsonIgnore]
        public TutoringProgram TutoringProgram { get; set; }
        [JsonIgnore]
        public ICollection<ActionPlan> ActionPlans { get; set; } = new List<ActionPlan>();
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        [JsonIgnore]
        public ICollection<TutorStudentProgram> TutorStudentPrograms { get; set; } = new List<TutorStudentProgram>();

    }

    public class Notificacion
    {
        public string resumen { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
    }
}
