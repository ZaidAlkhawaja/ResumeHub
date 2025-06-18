namespace ResumeHub.Models
{
    public class PortFolio : PersonalInfo
    {
        public int PortFolioId { get; set; }

        public string CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToString();

        public string? LastUpdatedDate { get; set; } 

        public bool IsDeleted { get; set; } = false;

        public int PortFolioTemplateId { get; set; }  


        public List<Skill>? Skills { get; set; } 

        public List<Service>? Services { get; set; }

        public List<Project>? Projects { get; set; }



        public string EndUserId { get; set; }  ///// Foreign key to the User table

        public EndUser EndUser { get; set; }  ///// Navigation property to the User table



    }
}
