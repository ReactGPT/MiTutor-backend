namespace MiTutor.Models.TutoringManagement
{
    public class Files
    {
        public int FilesId { get; set; }

        public string FilesName { get; set; }

        //public byte[] FilesContent { get; set; }

        public int AppointmentResultId { get; set; }

        public int PrivacyTypeId { get; set; }
    }

    public class FileBD
    {
        public int FilesId { get; set; }
        public string FilesName { get; set; }
        public bool Activo { get; set; }

    }
}
