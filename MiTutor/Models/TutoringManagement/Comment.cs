using System.Text.Json.Serialization;

namespace MiTutor.Models.TutoringManagement
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Message { get; set; }

        public bool IsActive { get; set; }

        public int AppointmentResultId { get; set; }

        public int PrivacyTypeId { get; set; }
        [JsonIgnore]
        public PrivacyType PrivacyType { get; set; }
    }
}
