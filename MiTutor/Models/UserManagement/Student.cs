using MiTutor.Models.UniversityUnitManagement;

namespace MiTutor.Models.GestionUsuarios
{
    public class Student : Person
    {
        public int Id { get; set; }
        public bool IsRisk { get; set; }
        public bool IsActive { get; set; }
        public int SpecialityId { get; set; }
        public string FacultyName { get; set; }
        public int IdTutor { get; set; }
        public string TutorName { get; set; }
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

    public class StudentTutoria
    {
        public int Id { get; set; }
        public bool IsRisk { get; set; }
        public bool IsActive { get; set; }
        public int SpecialityId { get; set; }
        public string FacultyName { get; set; }
        public Person Person { get; set; }
        public UserAccount Usuario { get; set; }
        public int IdTutor { get; set; }
        public string TutorName { get; set; }
    }


        //Indicadores 

        public class StudentContadorProgramasAcademicos
        {
            public int StudentId { get; set; }
            public string StudentName { get; set; }
            public string StudentLastName { get; set; }

            public string StudentSecondLastName { get; set; }

            public int CantidadProgramas { get; set; }


        }

        public class ListarCantidadAppointmentsStudent
        {
            public int StudentId { get; set; }
            public string StudentName { get; set; }
            public string StudentLastName { get; set; }

            public string StudentSecondLastName { get; set; }

            public int TotalAppointments { get; set; }

            public int RegisteredCount { get; set; }

            public int PendingResultCount { get; set; }

            public int CompletedCount { get; set; }
        }

        public class StudentTutoringProgram
        {
            public int TutoringProgramId { get; set; }
            public string ProgramName { get; set; }
            public int StudentCount { get; set; }
        }

        public class StudentAppointment
        {
            public int StudentId { get; set; }
            public string StudentName { get; set; }
            public string StudentLastName { get; set; }
            public string StudentSecondLastName { get; set; }
            public int AppointmentId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateOnly CreationDate { get; set; }
            public string Reason { get; set; }
            public int AppointmentTutorId { get; set; }
            public int AppointmentStatusId { get; set; }
            public string Classroom { get; set; }
            public int IsInPerson { get; set; }
            public string AppointmentStatusName { get; set; }
            public string FacultyName { get; set; }
            public int StudentCount { get; set; }
        }

        public class StudentProgramVirtualFace
        {
            public int CantidadPresenciales { get; set; }
            public int CantidadVirtuales { get; set; }
        }

        public class StudentTodo
        {
            public int PersonId { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string SecondLastName { get; set; }
            public string Phone { get; set; }
            public bool PersonIsActive { get; set; }
            public bool IsRisk { get; set; }
            public int SpecialityId { get; set; }
            public string SpecialtyName { get; set; }
            public string SpecialtyAcronym { get; set; }
            public int FacultyId { get; set; }
            public string FacultyName { get; set; }
            public string FacultyAcronym { get; set; }
            public string InstitutionalEmail { get; set; }
            public bool UserIsActive { get; set; }
            public string PUCPCode { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime ModificationDate { get; set; }
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
            
            public int AppointmentResultId { get; set; }
            public int TutoringProgramId { get; set; }

            public int AppointmentId { get; set;}

        }
    }
