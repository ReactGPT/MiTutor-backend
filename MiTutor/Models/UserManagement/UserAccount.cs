namespace MiTutor.Models.GestionUsuarios
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string InstitutionalEmail { get; set; }

        public string PUCPCode { get; set; }
        public bool IsActive { get; set; }
        public Person Persona { get; set; }
    }
}
