using ResumeHub.DTOs;

namespace ResumeHub.Interfaces
{
    public interface IAiLetterService
    {
        Task<CoverLetterResponseDto> GenerateAsync(CoverLetterRequestDto req);

        Task<string> GenerateThankYouAsync(string content);

    }
}
