using MiTutor.Models.GestionUsuarios;

namespace MiTutor.Models.TutoringManagement
{
    public class StudentProgram
    {
        public int StudentProgramId { get; set; } 
          
        public DateOnly JoinDate { get; set; }

        public bool IsActive { get; set; }

        public Student Student { get; set; }

        public TutoringProgram TutoringProgram { get; set; }

        public ICollection<ActionPlan> ActionPlans { get; set; } = new List<ActionPlan>();
        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<TutorStudentProgram> TutorStudentPrograms { get; set; } = new List<TutorStudentProgram>();

    }
}
