namespace ResumeHub.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string? ServiceName { get; set; } 

        public string? Description { get; set; } 


        public int PortfolioId { get; set; }  // Foreign key to the Portfolio table

        public PortFolio Portfolio { get; set; }  // Navigation property to the Portfolio table
    }
}
