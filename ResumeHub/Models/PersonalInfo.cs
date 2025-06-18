using System.ComponentModel.DataAnnotations;

namespace ResumeHub.Models
{
    public class PersonalInfo
    {
       [Required]  public string firstName { get; set; }
       [Required] public string lastName { get; set; }

        [EmailAddress] public string Email { get; set; }
        [Phone]  public string PhoneNumber { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        public string? address { get; set; }
        public string Summary { get; set; }

        public string? ImageBase64 { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageContentType { get; set; }
        public string? title { get; set; }

    
        // Additional properties can be added as needed
    }
}
