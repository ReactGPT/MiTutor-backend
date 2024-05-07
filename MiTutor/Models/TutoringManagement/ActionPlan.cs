namespace MiTutor.Models.TutoringManagement
{
    public class ActionPlan
    {
        public int ActionPlanId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }  

        public ICollection<Commitment> Commitments { get; set; } = new List<Commitment>();

        public StudentProgram StudentProgram { get; set; }

        public Tutor Tutor { get; set; }
    }
}
