namespace ResumeHub.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; } 
        public string? Description { get; set; } 
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public string? ImageBase64 { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageContentType { get; set; }

        public bool? IsOngoing { get; set; } = false;  ///// Indicates if the project is still ongoing
        public string? ProjectUrl { get; set; }  ///// This is the URL to the project repository or live demo

        public int? ResumeId { get; set; }  ///// Foreign key to the Resume
        public Resume? Resume { get; set; }  ///// Navigation property to the Resume table

        public int? PortFolioId { get; set; }  ///// Foreign key to the PortFolio table

        public PortFolio? PortFolio { get; set; }  ///// Navigation property to the PortFolio table

    }
}
