using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Models.GestionUsuarios
{
    public class Student:Person
    {
        public int Id { get; set; }
        public bool IsRisk { get; set; }
        public bool IsActive { get; set; }
        public int SpecialityId { get; set; }
        public Specialty Specialty { get; set; }
    }

    public class ListarStudentJSON
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string PUCPCode { get; set; }
        public string InstitutionalEmail { get; set; }
        public string Phone { get; set; }
        public string SpecialtyName { get; set; }
        public string FacultyName { get; set; }
    }

    public class ListarStudentJSON2
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string PUCPCode { get; set; }
        public bool IsRisk { get; set; }
        public bool Asistio { get; set; }
    }
}
