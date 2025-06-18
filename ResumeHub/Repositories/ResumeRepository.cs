using Microsoft.EntityFrameworkCore;
using ResumeHub.Data;
using ResumeHub.Models;

namespace ResumeHub.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        // This class will implement methods to interact with the resume data.
        // For example, methods to add, update, delete, and retrieve resumes.

        private readonly ApplicationDbContext _Context;  // Database context to interact with the database.
        public ResumeRepository(ApplicationDbContext context) 
        {
            // Constructor to initialize the repository with the database context.
            // This context will be used to interact with the database.
            _Context = context;

        }
        public void AddResume(Resume resume)
        {
            // Implementation for adding a resume to the database .

            _Context.Resumes.Add(resume);  // Add the resume to the Resumes DbSet in the context.
            _Context.SaveChanges();        // Save changes to the database.

        }

        public async Task DeleteResume(int id)
        {
            // Implementation for deleting a resume by ID.
            
            var resume = _Context.Resumes.Find(id);  // Find the resume by ID in the Resumes DbSet.
            if (resume != null)
            {
                resume.IsDeleted = true;  // Mark the resume as deleted by setting IsDeleted to true.
              
                _Context.Resumes.Update(resume);  // Update the resume in the DbSet.

                 await _Context.SaveChangesAsync();         // Save changes to the database.
            }

        }

        public List<Resume> GetResumesByEndUserId(string endUserId)
        {
            // Implementation for retrieving a resume by EndUserId.

            return _Context.Resumes.Where(x => x.EndUserId == endUserId).ToList();  // Find the first resume with the specified EndUserId.

        }

        public async Task <Resume> GetResumeById(int id)
        {
           return await _Context.Resumes
                    .Include(r => r.Experiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Projects)
                    .Include(r => r.Skills)
                    .Include(r => r.Certifications)
                    .Include(r => r.Languages).FirstOrDefaultAsync(r => r.ResumeId == id);  // Find and return the resume by ID from the Resumes DbSet.
        }

        public async Task UpdateResume(Resume resume)
        {

            try
            {
                var existingResume = await _Context.Resumes
                    .Include(r => r.Experiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Projects)
                    .Include(r => r.Skills)
                    .Include(r => r.Certifications)
                    .Include(r => r.Languages)
                    .FirstOrDefaultAsync(r => r.ResumeId == resume.ResumeId);
                if (existingResume == null)
                {
                    throw new KeyNotFoundException($"Resume with ID {resume.ResumeId} not found.");
                }
                // Remove related entities
                _Context.Experiences.RemoveRange(existingResume.Experiences);
                _Context.Educations.RemoveRange(existingResume.Educations);
                _Context.Projects.RemoveRange(existingResume.Projects);
                _Context.Skills.RemoveRange(existingResume.Skills);
                _Context.Certifications.RemoveRange(existingResume.Certifications);
                _Context.Languages.RemoveRange(existingResume.Languages);
                // Remove the resume itself
                _Context.Resumes.Remove(existingResume);

                // Add the updated resume
                _Context.Resumes.Add(resume);  // Add the updated resume to the Resumes DbSet.
                await _Context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the resume.", ex);

            }



            //    _Context.Resumes.Update(resume);  // Update the resume in the Resumes DbSet.
            //resume.LastUpdatedDate = DateOnly.FromDateTime(DateTime.Now).ToString();  // Update the LastUpdatedDate to the current date.
            //await _Context.SaveChangesAsync();           // Save changes to the database.

        }
    }
}




