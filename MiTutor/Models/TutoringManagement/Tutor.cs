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
}
