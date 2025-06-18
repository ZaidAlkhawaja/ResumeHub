namespace ResumeHub.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string? LanguageName { get; set; } 
        public string? ProficiencyLevel { get; set; }

        public int ResumeId { get; set; }  ///// This is the foreign key to the Resume table

        public Resume Resume { get; set; }  ///// This is the navigation property to the Resume table


    }
}
