using ResumeHub.DTOs;
using ResumeHub.Models;

namespace ResumeHub.Extensions
{
    public static class ResumeExtensions
    {
        public static Models.Resume MapToResumeEntity(ResumeJsonDto dto, string userId)
        {
            return new Models.Resume
            {
                ResumeId = dto.Id,
                address = dto.Address,
                firstName = dto.FirstName,
                lastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                title = dto.Title,
                Summary = dto.Bio ?? dto.Bio, // use whichever is populated
                GitHubProfile = dto.GitHubLink,
                LinkedInProfile = dto.LinkedinLink,
                ResumeTemplateId = dto.ResumeTemplateId, // assuming you have a template ID field
                EndUserId = userId, // assuming you use ASP.NET Identity

                // Date/time fields:
                CreatedDate = DateTime.UtcNow.ToString(),
                LastUpdatedDate = DateTime.UtcNow.ToShortDateString(),

                // Collections, mapped (you may need to adjust for navigation properties):
                Educations = dto.Educations?.Select(e => new Education
                {
                    CollegeName = e.CollegeName,
                    DegreeType = e.DegreeField,
                    Major = e.Major,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    GPA = e.GPA
                }).ToList() ?? new List<Education>(),

                Experiences = dto.Experiences?.Select(x => new Experience
                {
                    JobTitle = x.Title,
                    CompanyName = x.Company,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsCurrentJob = x.IsCurrent ?? false,
                    JobDescription = x.Duties
                }).ToList() ?? new List<Experience>(),

                Skills = dto.Skills?.Select(s => new Skill
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType
                }).ToList() ?? new List<Skill>(),

                Certifications = dto.Certificates?.Select(c => new Certification
                {

                    IssuingOrganization = c.ProviderName,
                    IssuedDate = c.StartDate,
                    ExpirationDate = c.EndDate,
                    Field = c.Field,

                }).ToList() ?? new List<Certification>(),

                Projects = dto.Projects?.Select(p => new Project
                {
                    ProjectName = p.ProjectName,
                    Description = p.ProjectDescription,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ProjectUrl = p.ProjectLink
                }).ToList() ?? new List<Project>(),

                Languages = dto.Languages?.Select(l => new Language
                {
                    LanguageName = l.LanguageName,
                    ProficiencyLevel = l.Level
                }).ToList() ?? new List<Language>()
            };
        }

        public static ResumeJsonDto MapToResumeJsonDto(Models.Resume resume)
        {
            if (resume == null) return null;

            return new ResumeJsonDto
            {
                Id = resume.ResumeId,
                FirstName = resume.firstName,
                LastName = resume.lastName,
                Email = resume.Email,
                PhoneNumber = resume.PhoneNumber,
                Created = resume.CreatedDate,
                LastEdit = resume.LastUpdatedDate,
                Address = resume.address,
                Bio = resume.Summary,
                Title = resume.title,
                GitHubLink = resume.GitHubProfile,
                LinkedinLink = resume.LinkedInProfile,
                ResumeTemplateId = resume.ResumeTemplateId, // Assuming you have a template ID field
                Educations = resume.Educations?.Select(e => new EducationItem
                {
                    CollegeName = e.CollegeName,
                    DegreeField = e.DegreeType,
                    Major = e.Major,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    GPA = e.GPA
                }).ToList(),
                Experiences = resume.Experiences?.Select(x => new ExperienceItem
                {
                    Company = x.CompanyName,
                    Title = x.JobTitle,
                    Duties = x.JobDescription,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsCurrent = x.IsCurrentJob
                }).ToList(),
                Skills = resume.Skills?.Select(s => new SkillItem
                {
                    SkillName = s.SkillName,
                    SkillType = s.SkillType
                }).ToList(),
                Certificates = resume.Certifications?.Select(c => new CertificateItem
                {
                    ProviderName = c.IssuingOrganization,
                    Field = c.Field,
                    StartDate = c.IssuedDate,
                    EndDate = c.ExpirationDate
                }).ToList(),
                Projects = resume.Projects?.Select(p => new ProjectItem
                {
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ProjectLink = p.ProjectUrl
                }).ToList(),
                Languages = resume.Languages?.Select(l => new LanguageItem
                {
                    LanguageName = l.LanguageName,
                    Level = l.ProficiencyLevel
                }).ToList()
            };
        }

        public static List<ResumeJsonDto> ConvertResumesToJsonDtos(List<Models.Resume> resumes)
        {
            return resumes.Select(resume => new ResumeJsonDto
            {
                Id = resume.ResumeId,
                FirstName = resume.firstName,
                LastName = resume.lastName,
                Email = resume.Email,
                PhoneNumber = resume.PhoneNumber,
                Title = resume.title,
                Bio = resume.Summary,
                LinkedinLink = resume.LinkedInProfile,
                GitHubLink = resume.GitHubProfile,
                ResumeTemplateId = resume.ResumeTemplateId, // Assuming you have a template ID field
                Educations = resume.Educations?.Select(e => new EducationItem
                {
                    CollegeName = e.CollegeName,
                    DegreeField = e.DegreeType,
                    Major = e.Major,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    GPA = e.GPA
                }).ToList(),
                Experiences = resume.Experiences?.Select(x => new ExperienceItem
                {
                    Company = x.CompanyName,
                    Title = x.JobTitle,
                    Duties = x.JobDescription,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsCurrent = x.IsCurrentJob
                }).ToList(),
                Skills = resume.Skills?.Select(s => new SkillItem
                {
                    SkillName = s.SkillName
                    // SkillType mapping if available
                }).ToList(),
                Certificates = resume.Certifications?.Select(c => new CertificateItem
                {
                    ProviderName = c.IssuingOrganization,
                    Field = c.Field,
                    StartDate = c.IssuedDate,
                    EndDate = c.ExpirationDate
                }).ToList(),
                Projects = resume.Projects?.Select(p => new ProjectItem
                {
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ProjectLink = p.ProjectUrl
                }).ToList(),
                Languages = resume.Languages?.Select(l => new LanguageItem
                {
                    LanguageName = l.LanguageName,
                    Level = l.ProficiencyLevel
                }).ToList()
            }).ToList();
        }


    }
}
