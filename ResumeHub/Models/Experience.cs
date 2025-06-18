namespace ResumeHub.Models
{
    public class Experience
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; } 

        public string? JobTitle { get; set; }

        public string? JobDescription { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public bool? IsCurrentJob { get; set; } 

        public int ResumeId { get; set; }  ///// Foreign key to the Resume
        public Resume Resume { get; set; }  ///// Navigation property to the Resume table

  
    }
}
