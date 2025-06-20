using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResumeHub.Models;

namespace ResumeHub.Data;

public class ApplicationDbContext : IdentityDbContext <Person>  // Person is the custom user class that inherits from IdentityUser
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resume> Resumes { get; set; }
    public DbSet<PortFolio> Portfolios { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<EndUser> EndUsers { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public DbSet<Review> Reviews { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Define relationships and constraints if needed
        modelBuilder.Entity<Resume>()
            .HasOne(r => r.EndUser)
            .WithMany(u => u.Resumes)
            .HasForeignKey(r => r.EndUserId)
            .OnDelete(DeleteBehavior.Cascade); 
     

        modelBuilder.Entity<PortFolio>()
            .HasOne(p => p.EndUser)
            .WithMany(u => u.PortFolios)
            .HasForeignKey(p => p.EndUserId).OnDelete(DeleteBehavior.Cascade); 
        
    }

}
