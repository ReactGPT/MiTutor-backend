namespace MiTutor.Models.Utils
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public long Uptime { get; set; }
        public DateTime Timestamp { get; set; }
        public Dependencies Dependencies { get; set; }
    }

    public class Dependencies
    {
        public ServiceStatus Database { get; set; }
    }

    public class ServiceStatus
    {
        public string Status { get; set; }
        public int ResponseTimeMs { get; set; }
    }

}
