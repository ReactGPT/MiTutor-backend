using System.Text.Json.Serialization;

namespace MiTutor.Models.GestionUsuarios
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public UserAccount Usuario { get; set; }
    }
}
