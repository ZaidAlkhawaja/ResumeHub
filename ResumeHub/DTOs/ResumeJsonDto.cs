﻿using System.ComponentModel.DataAnnotations;

namespace ResumeHub.DTOs
{
    public class 
        ResumeJsonDto
    {
            // PersonalInfo

           public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

       
        public int ResumeTemplateId { get; set; } = 1; // Default template ID

           public string Created { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToString();
            public string? LastEdit { get; set; }

             public string? Address { get; set; }
            public string? Bio { get; set; }
            public string? Title { get; set; }
            public string? GitHubLink { get; set; }
            public string? LinkedinLink { get; set; }

            // Parsed arrays (each array can be null or empty if OpenAI couldn’t extract any)
            public List<EducationItem>? Educations { get; set; }
            public List<ExperienceItem>? Experiences { get; set; }
            public List<SkillItem>? Skills { get; set; }
            public List<LanguageItem>? Languages { get; set; }
            public List<CertificateItem>? Certificates { get; set; }
            public List<ProjectItem>? Projects { get; set; }

        }

        public class EducationItem
        {
            public string? CollegeName { get; set; }
            public string? DegreeField { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public string? Major { get; set; }
            public double? GPA { get; set; }

            public bool? IsCurrent { get; set; }  // Indicates if the education is ongoing
    }

        public class ExperienceItem
        {
            public string? Title { get; set; }
            public string? Company { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public bool? IsCurrent { get; set; }
            public string? Duties { get; set; }
        }

        public class SkillItem
        {
            public string? SkillName { get; set; }
            public string? SkillType { get; set; }
        }

        public class LanguageItem
        {
            public string? LanguageName { get; set; }
            public string? Level { get; set; }
        }

        public class CertificateItem
        {
            public string? ProviderName { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public string? Field { get; set; }
            public double? GPA { get; set; }
        }

        public class ProjectItem
        {
            public string? ProjectName { get; set; }
            public string? ProjectDescription { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public string? ProjectLink { get; set; }

             public bool? IsOngoing { get; set; } = false;  ///// Indicates if the project is still ongoing

    }
    }
    

