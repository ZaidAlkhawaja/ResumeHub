namespace ResumeHub.Models
{
    public class EndUser : Person
    {
       

        public List<Resume> Resumes { get; set; }

        public List<PortFolio> PortFolios { get; set; }

        public List<Review> Reviews { get; set; }

    }
}
