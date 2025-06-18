namespace ResumeHub.Models
{
    public class Certification
    {
        public int Id { get; set; }

        public string? CertificateName { get; set; }

        public string? IssuingOrganization { get; set; }

        public string? CertificateFiled { get; set; }

        public string? Field { get; set; }

        public string? IssuedDate { get; set; }

        public string? ExpirationDate { get; set; }

        public int ResumeId { get; set; }  // Foreign key to the Resume
        public Resume Resume { get; set; }  // Navigation property to the Resume table


    }
}
