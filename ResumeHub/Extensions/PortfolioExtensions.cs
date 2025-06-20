using ResumeHub.DTOs;
using ResumeHub.Models;

namespace ResumeHub.Extensions
{
    public static class PortfolioExtensions
    {
        public static List<PortfolioJsonDto> ConvertPortFolioToJsonDtos(List<PortFolio> portfolios)
        {
            if (portfolios == null || portfolios.Count == 0)
                return new List<PortfolioJsonDto>();
            return portfolios.Select(portfolio => new PortfolioJsonDto
            {
                Id = portfolio.PortFolioId,
                Title = portfolio.title,
                FirstName = portfolio.firstName,
                LastName = portfolio.lastName,
                Email = portfolio.Email,
                PhoneNumber = portfolio.PhoneNumber,
                Address = portfolio.address,
                Summery = portfolio.Summary,
                CreatedDate = portfolio.CreatedDate,
                LastUpdatedDate = portfolio.LastUpdatedDate,
                GitHubLink = portfolio.GitHubProfile,
                LinkedinLink = portfolio.LinkedInProfile,
                PortFolioTemplateId = portfolio.PortFolioTemplateId, // Assuming you have a template ID field
                ImageBase64 = portfolio.ImageBase64, // Assuming ProfilePicture is a base64 string
                ImageFileName = portfolio.ImageFileName, // You can set this if you have a file name
                ImageContentType = portfolio.ImageContentType, // You can set this if you have a content type
                Services = portfolio.Services?.Select(s => new ServiceItem
                {
                    ServiceName = s.ServiceName,
                    ServiceDescription = s.Description
                }).ToList() ?? new List<ServiceItem>(),
                Projects = portfolio.Projects?.Select(proj => new ProjectItem1
                {
                    ProjectName = proj.ProjectName,
                    ProjectDescription = proj.Description,
                    StartDate = proj.StartDate,
                    EndDate = proj.EndDate,
                    IsOngoing = proj.IsOngoing,
                    ProjectLink = proj.ProjectUrl,
                    ImageBase64 = proj.ImageBase64,
                    ImageFileName = proj.ImageFileName,
                    ImageContentType = proj.ImageContentType



                }).ToList() ?? new List<ProjectItem1>(),
                Skills = portfolio.Skills?.Select(s => new SkillItem
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType // Assuming SkillType is a string, adjust if it's an enum or different type
                }).ToList() ?? new List<SkillItem>()


            }).ToList();

        }

        public static PortfolioJsonDto MapToPortfolioJsonDto(PortFolio portfolio)
        {
            return new PortfolioJsonDto
            {
                Id = portfolio.PortFolioId,
                // Top-level properties:
                FirstName = portfolio.firstName,
                LastName = portfolio.lastName,
                Email = portfolio.Email,
                PhoneNumber = portfolio.PhoneNumber,
                Title = portfolio.title,
                Address = portfolio.address,
                Summery = portfolio.Summary,
                GitHubLink = portfolio.GitHubProfile,
                LinkedinLink = portfolio.LinkedInProfile,
                PortFolioTemplateId = portfolio.PortFolioTemplateId, // Assuming you have a template ID field
                ImageBase64 = portfolio.ImageBase64,
                ImageFileName = portfolio.ImageFileName,
                ImageContentType = portfolio.ImageContentType,

                // Services
                Services = portfolio.Services?.Select(s => new ServiceItem
                {
                    ServiceName = s.ServiceName,
                    ServiceDescription = s.Description
                }).ToList() ?? new List<ServiceItem>(),

                // Projects
                Projects = portfolio.Projects?.Select(p => new ProjectItem1
                {
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsOngoing = p.IsOngoing,
                    ProjectLink = p.ProjectUrl,
                    ImageBase64 = p.ImageBase64,
                    ImageFileName = p.ImageFileName,
                    ImageContentType = p.ImageContentType
                }).ToList() ?? new List<ProjectItem1>(),

                // Skills
                Skills = portfolio.Skills?.Select(s => new SkillItem
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType
                }).ToList() ?? new List<SkillItem>(),

                // Timestamps - if you need to include them in the DTO
                CreatedDate = portfolio.CreatedDate,
                LastUpdatedDate = portfolio.LastUpdatedDate
            };
        }

        public static PortFolio MapToPortFolioEntity(PortfolioJsonDto dto, string userId)
        {
            return new PortFolio
            {

                PortFolioId = dto.Id,

                firstName = dto.FirstName,
                lastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                title = dto.Title,
                address = dto.Address,
                Summary = dto.Summery,
                GitHubProfile = dto.GitHubLink,
                LinkedInProfile = dto.LinkedinLink,
                PortFolioTemplateId = dto.PortFolioTemplateId, // Assuming you have a template ID field
                ImageBase64 = dto.ImageBase64, // Assuming ImageBase64 is a base64 string
                ImageFileName = dto.ImageFileName,
                ImageContentType = dto.ImageContentType,
                EndUserId = userId,
                // Timestamps
                CreatedDate = DateTime.UtcNow.ToString(),
                LastUpdatedDate = DateTime.UtcNow.ToShortDateString(),
                IsDeleted = false,
                // Services
                Services = dto.Services?.Select(s => new Service
                {
                    ServiceName = s.ServiceName,
                    Description = s.ServiceDescription
                }).ToList() ?? new List<Service>(),
                // Projects
                Projects = dto.Projects?.Select(p => new Project
                {
                    ProjectName = p.ProjectName,
                    Description = p.ProjectDescription,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    IsOngoing = p.IsOngoing ?? false, // Default to false if null
                    ProjectUrl = p.ProjectLink,
                    ImageBase64 = p.ImageBase64, // Assuming ImageBase64 is a base64 string
                    ImageFileName = p.ImageFileName,
                    ImageContentType = p.ImageContentType

                }).ToList() ?? new List<Project>(),
                // Skills
                Skills = dto.Skills?.Select(s => new Skill
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType // Assuming SkillType is a string, adjust if it's an enum or different type
                }).ToList() ?? new List<Skill>()
            };
        }



    }
}
