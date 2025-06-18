using ResumeHub.DTOs;

namespace ResumeHub.Interfaces
{
    public interface IResumeOpenAi
    {
        Task<ResumeJsonDto> ParseResumeAsync(ResumeDTO resemeRowData);

    }
}
