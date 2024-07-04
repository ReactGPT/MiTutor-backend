using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Models.GestionUsuarios
{
    public class UserStudent:UserGeneric
    {
        public bool IsStudent { get; set; }
        public bool IsRisk { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
    public class UserGeneric
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string RolName { get; set; }  
        public string Type { get; set; }

    }
    public class UserGenericTutor : UserGeneric
    {
        public bool IsTutor { get; set; }
        public int TutorId { get; set; }
        public string MeetingRoom { get; set; }

    }
    public class UserGenericManager: UserGeneric
    {
        public bool IsManager { get; set; }
        //public ManagerDetail ManagingDepartment { get; set; }
        public int ManagerId { get; set; }
        public string DepartmentType { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentAcronym { get; set; }
    }
    public class UserAdmin : UserGeneric
    {
        public bool IsAdmin { get; set; }
    }
    public class UserDerivation: UserGeneric
    {
        public bool IsDerivation { get; set; }
    }
    public class UserCaringManager : UserGeneric
    {
        public bool IsCaringManager { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
    public class UserAccount
    {
        public int? Id { get; set; } //puse el ?
        public string InstitutionalEmail { get; set; }
        public string PUCPCode { get; set; }
        public bool IsActive { get; set; }
        //public int AccountTypeId { get; set; }
        //public string AccountTypeDescription { get; set; }
        public Person Persona { get; set; }
        //public bool IsStudent { get; set; }
        //public UserStudent StudentDetail { get; set; }
        //public bool IsGeneric { get; set; }
        public List<object> Roles { get; set; }
        public bool isVerified { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }

    public class AccountType
    {
        public int Id { get; set; }
        public string description { get; set; }
    }
    
}
