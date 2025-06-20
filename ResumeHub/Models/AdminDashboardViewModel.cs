using ResumeHub.DTOs;


namespace ResumeHub.Models
{
public class AdminDashboardViewModel
    {
        public int TotalResumes { get; set; }
        public int TotalPortfolios { get; set; }
        public int TotalUsers { get; set; }
        public List<Person?> RecentUsers { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Resume> RecentResumes { get; set; }
        public List<PortFolio> RecentPortfolios { get; set; }
    }
  

}