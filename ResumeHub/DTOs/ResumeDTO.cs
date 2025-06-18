using System.ComponentModel.DataAnnotations;

namespace ResumeHub.DTOs
{
    public class ResumeDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Url(ErrorMessage = "Invalid LinkedIn URL")]
        public string? LinkedIn { get; set; }

        [Url(ErrorMessage = "Invalid GitHub URL")]
        public string? GitHub { get; set; }

        [Required(ErrorMessage = "Professional title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Professional summary is required")]
        [StringLength(1000, ErrorMessage = "Summary cannot exceed 1000 characters")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Work experience is required")]
        [StringLength(2000, ErrorMessage = "Experience cannot exceed 2000 characters")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Education is required")]
        [StringLength(1000, ErrorMessage = "Education cannot exceed 1000 characters")]
        public string Education { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        [StringLength(500, ErrorMessage = "Skills cannot exceed 500 characters")]
        public string Skills { get; set; }

        public int ResumeTemplateId { get; set; } = 1 ;


        public int CurrentStep { get; set; } = 1 ;

        //[Required]
        //public bool ConsentGiven { get; set; }


    }
}
