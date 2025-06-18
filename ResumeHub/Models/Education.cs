namespace ResumeHub.Models
{
    public class Education
    {
        public int Id { get; set; }

        public string? CollegeName { get; set; } 

        public string? DegreeType { get; set; }

        public string? Major { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }


        public bool? IsCurrentEducation { get; set; } 

        public double? GPA { get; set; }

        public int ResumeId { get; set; }  // Foreign key to the Resume

        public Resume Resume { get; set; }  // Navigation property to the Resume table


    }
}
