namespace MiTutor.Models.TutoringManagement
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public NotificationType NotificationType { get; set; }
    }
}
