namespace MiTutor.Models.TutoringManagement
{
    public class Commitment
    {
        public int CommitmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; } 

        public ActionPlan ActionPlan { get; set; }

        public CommitmentStatus CommitmentStatus { get; set; }

    }
}
