using ResumeHub.Models;

namespace ResumeHub.Interfaces
{
    public interface IReviewRepo
    {
        Task<List<Review>> GetRecentAsync(int count = 5);

        Task AddAsync(Review review);
        Task DeleteAsync(int id);
        Task<int> GetCountAsync();

        Task<List<Review>> GetReviewsCount(int Count);

        Task UpdateAsync(Review review);

        

    }
}
