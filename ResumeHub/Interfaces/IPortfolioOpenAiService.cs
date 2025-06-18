using ResumeHub.DTOs;

namespace ResumeHub.Interfaces
{
    public interface IPortfolioOpenAiService
    {
        Task<PortfolioJsonDto> ParsePortfolioDataAsync(PortfolioDTO dto) ; 
    }
}
