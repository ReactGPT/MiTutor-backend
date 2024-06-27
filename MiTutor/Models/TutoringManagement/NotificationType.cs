namespace MiTutor.Models.TutoringManagement
{
    public class NotificationType
    {
        public int NotificationTypeId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public ICollection<Notification> Notifications { get; set; }
    }
}
