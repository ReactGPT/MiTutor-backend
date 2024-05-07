using System.Xml.Linq;

namespace MiTutor.Models.TutoringManagement
{
    public class PrivacyType
    {
        public int PrivacyTypeId { get; set; }

        public string Abbreviation { get; set; }

        public bool IsActive { get; set; }
     }
}
