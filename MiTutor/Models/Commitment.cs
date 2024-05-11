namespace MiTutor.Models
{
    public class Commitment
    {
        public int CommitmentId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ActionPlanId { get; set; }
        public int CommitmentStatusId { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ModificationDate { get; set; }

        public string CommitmentStatusDescription { get; set; }
    }
}
