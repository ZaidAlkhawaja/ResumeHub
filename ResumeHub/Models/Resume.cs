namespace ResumeHub.Models
{
    public class Resume : PersonalInfo
    {
        public int ResumeId { get; set; }
        public string CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToString();

        public string? LastUpdatedDate { get; set; } 

        public bool? IsDeleted { get; set; } = false;

        public int ResumeTemplateId { get; set; }

        public List<Experience> Experiences { get; set; } = new List<Experience>();


        public List<Education> Educations { get; set; } = new List<Education>();

        public List<Project> Projects { get; set; }  = new List<Project>();

        public List<Skill> Skills { get; set; } = new List<Skill>();

        public List<Certification> Certifications { get; set; } = new List<Certification>();

        public List<Language> Languages { get; set; } = new List<Language>();

        public string EndUserId { get; set; }  ///// Foreign key to the User table

        public EndUser EndUser { get; set; }  ///// Navigation property to the User table








    }
}
