namespace MiTutor.Models.GestionUsuarios
{
    public class Student:Person
    {
        public int Id { get; set; }
        public bool IsRisk { get; set; }
        public bool IsActive { get; set; }
        public int SpecialityId { get; set; }
        public string FacultyName { get; set; }

        public int IdTutor { get; set; }
        public string TutorName { get; set; }
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

    public class StudentIdVerified
    {
        public int studentId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string secondLastName { get; set; }
        public bool isActive { get; set; }
        public string pucpCode { get; set; }
        public string institutionalEmail { get; set; }
        public string facultyName { get; set; }
        public bool isRegistered { get; set; }
    }
}
