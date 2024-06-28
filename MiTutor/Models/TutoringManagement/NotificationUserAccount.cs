using MiTutor.Models.GestionUsuarios;

namespace MiTutor.Models.TutoringManagement
{
    public class NotificationUserAccount
    {
        public int NotificationUserAccountId { get; set; }
        public int? NotificationId { get; set; }
        public int? UserAccountId { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public Notification Notification { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
