namespace ResumeHub.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string? SkillName { get; set; }

        public string? SkillType { get; set; } = string.Empty;

        public int? ResumeId { get; set; }  ///// This is the foreign key to the Resume table

        public Resume? Resume { get; set; }  ///// This is the navigation property to the Resume table

        public int? PortFolioId { get; set; }  ///// This is the foreign key to the PortFolio table

        public PortFolio? PortFolio { get; set; }  ///// This is the navigation property to the PortFolio table


    }
}
