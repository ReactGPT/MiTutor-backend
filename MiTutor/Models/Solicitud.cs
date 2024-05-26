namespace MiTutor.Models.TutoringManagement
{
    public class Solicitud
    {
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Especialidad { get; set; }
        public string Tutor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
    }
}
