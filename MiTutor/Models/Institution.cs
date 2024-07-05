namespace MiTutor.Models
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string InstitutionType { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
    }
}
