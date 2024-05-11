namespace MiTutor.Models
{
    public class ActionPlan
    {
        public int ActionPlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int StudentProgramId { get; set; }
        public int TutorId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
