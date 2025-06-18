using ResumeHub.DTOs;

namespace ResumeHub.Interfaces
{
    public interface IResumePdfGenerator
    {
        byte[] GenerateResumePdf(ResumeJsonDto resume);
    }
}
