using ResumeHub.Models;

namespace ResumeHub.Repositories

{
    public interface IResumeRepository
    {
        // Method to add a new resume
        void AddResume(Resume resume);

        // Method to update an existing resume
        Task UpdateResume(Resume resume);

        // Method to delete a resume by ID
        Task DeleteResume(int id);
        // Method to retrieve a resume by ID
        Task<Resume> GetResumeById(int id);

        // Method to retrieve a resume by User ID
       List<Resume> GetResumesByEndUserId(string endUserId);


    }
}
