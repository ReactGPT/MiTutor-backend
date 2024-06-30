using MiTutor.Models.GestionUsuarios;
using MiTutor.Models.UniversityUnitManagement;
using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Tutor
    {
        public int TutorId { get; set; }

        public string MeetingRoom { get; set; }

        public bool IsActive { get; set; }

        public string ModificationDate { get; set; }
        public UserAccount UserAccount { get; set; }
        [JsonIgnore]
        public ICollection<AvailabilityTutor> AvailabilityTutors { get; set; } = new List<AvailabilityTutor>();
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public Faculty Faculty { get; set; }

        public TutoringProgram TutoringProgram { get; set; } 
    }

    public class TutorXtutoringProgramXalumno
    { 
        public int TutorId  { get; set; }
        public string TutorName { get; set;}
        public string TutorLastName { get; set;}
        public string TutorSecondLastName { get; set; }
        public string State { get;set;}


    }

    //Indicadores 

    public class TutorContadorProgramasAcademicos
    {
        public int TutorId { get; set; }
        public string TutorName { get; set; }
        public string TutorLastName { get;set; }

        public string TutorSecondLastName { get; set;}

        public int CantidadProgramas { get; set; }


    }

    //Indicador

    public class ListarCantidadAppointment
    {
        public int TutorId { get; set; }
        public string TutorName { get; set; }
        public string TutorLastName { get; set; }

        public string TutorSecondLastName { get; set; }

        public int TotalAppointments { get; set; }

        public int RegisteredCount { get; set; }    

        public int PendingResultCount { get; set; }

        public int CompletedCount { get; set; }
    }

    //INDICADOR TUTOR-DETALLE
    public class TutorProgram
    {
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public int StudentCount { get; set; }
    }


    //INDICADOR CITA-DETALLE
    public class TutorAppointment
    {
        public int TutorId { get; set; }
        public string TutorName { get; set; }
        public string TutorLastName { get; set; }
        public string TutorSecondLastName { get; set; }
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

    public class TutorProgramVirtualFace 
    {
        public int CantidadPresenciales { get; set; }
        public int CantidadVirtuales { get; set; }
    }


    //Update
    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Phone { get; set; }
        public string SpecialtyName { get; set; }
        public string FacultyName { get; set; }
    }



    public class StudentCountTutor
    {
        public int StudentCount { get; set; }
    }


    public class TutoringProgramInfo
    {
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public bool FaceToFace { get; set; }
        public bool Virtual { get; set; }
        public string FacultyName { get; set; }
        public string SpecialtyName { get; set; }
    }


    public class AppointmentInfo
    {
        public int AppointmentId { get; set; }
        public int TutorId { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentSecondLastName { get; set; }
        public bool IsActive { get; set; }
    }



    public class StudentProgramInfo
    {
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public bool FaceToFace { get; set; }
        public bool Virtual { get; set; }
        public string FacultyName { get; set; }
        public string SpecialtyName { get; set; }
    }


    public class StudentAppointmentInfo
    {
        public int AppointmentId { get; set; }
        public int TutorId { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentSecondLastName { get; set; }
        public bool IsActive { get; set; }
    }


    public class AppointmentCountTutor
    {
        public int AppointmentCount { get; set; }
    }

    public class StudentDetailedInfo
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Phone { get; set; }
        public string SpecialtyName { get; set; }
        public string FacultyName { get; set; }
        public int TutoringProgramId { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
    }



}
