namespace MiTutor.Models.TutoringManagement
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Message { get; set; }

        public bool IsActive { get; set; }

        public int AppointmentResultId { get; set; }

        public PrivacyType PrivacyType { get; set; }
    }
}
