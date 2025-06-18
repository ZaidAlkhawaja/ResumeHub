using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using ResumeHub.DTOs;
using ResumeHub.Interfaces;
using ResumeHub.Models;
using ResumeHub.Repositories;
using ResumeHub.Services.PdfGeneration;
using System.Security.Claims;
using System.Threading.Tasks;




namespace ResumeHub.Controllers
{
    public class ResumeController : Controller
    {
        public IResumeOpenAi _service { get; set; }
        public IResumeRepository _repo { get; set; }

        private readonly IResumePdfGenerator _pdfGenerator;
        public ResumeController(IResumeOpenAi service, IResumeRepository repo , IResumePdfGenerator pdfGenerator) {

            // Ideally, you would inject this service via Dependency Injection
            // For simplicity, we are instantiating it directly here
            _service = service ;
            _repo = repo;
            _pdfGenerator = pdfGenerator;
        }

        [Authorize]

        public async Task<IActionResult> Index()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Fetch resumes for the current user
            var resumes =  _repo.GetResumesByEndUserId(userId);
            // Convert resumes to JSON DTOs
            var resumeDtos = ConvertResumesToJsonDtos(resumes);
            return View(resumeDtos);
        }
        private ResumeJsonDto MapToResumeJsonDto(Resume resume)
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

        public async Task<IActionResult> DownloadResume (int Id)
        {
            var resumeEntity = await _repo.GetResumeById(Id);
            if (resumeEntity == null)
            {
                return NotFound();
            }
            var resumeDto = MapToResumeJsonDto(resumeEntity);
            try
            {
                var pdfBytes = _pdfGenerator.GenerateResumePdf(resumeDto);
                return File(pdfBytes, "application/pdf",$"{resumeDto.FirstName}_{resumeDto.LastName}_Resume.pdf");
            }
            catch (Exception ex)
            {
                // Log error herereturn
                return StatusCode(500, $"PDF generation failed: {ex.Message}");
            }


        }

        [HttpGet]
        public async Task<IActionResult> DeleteResume(int ID)
        {
            await _repo.DeleteResume(ID);
            
            return RedirectToAction ("Index");
        }


        [HttpGet]
        public async Task<IActionResult> EditResume(int ID)
        {
            var r = await _repo.GetResumeById(ID);
            var resume = MapToResumeJsonDto(r);
            return View(resume);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateResume(ResumeJsonDto resumeDto) // Rename parameter
        {
            if (!ModelState.IsValid)
            {
                return View("EditResume", resumeDto);
            }

            Resume resumeEntity = MapToResumeEntity(resumeDto, User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            await _repo.UpdateResume(resumeEntity); // Add await

            return RedirectToAction("Index"); // Redirect to reload data
        }

        private Resume MapToResumeEntity(ResumeJsonDto dto, string userId)
        {
            return new Resume
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
        private List<ResumeJsonDto> ConvertResumesToJsonDtos(List<Resume> resumes)
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

        public IActionResult CreateNewResume() 
        {
            return View(new ResumeDTO { CurrentStep = 1 });
        }

        public async Task< IActionResult> ResumesTemplates(int ID)
        {
            var resume =  await _repo.GetResumeById(ID);
            if (resume == null)
            {
                return NotFound();
            }
            var dto = MapToResumeJsonDto(resume);
            return View(dto);
        }


        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessStep(ResumeDTO model, string command)   
        {
            if (command == "prev")
            {
                // Move to previous step
                model.CurrentStep--;
                return View("CreateNewResume", model);
            }

            if (command == "next")
            {
                // Move to next step
                // Validate current step before proceeding
                model.CurrentStep++;
                return View("CreateNewResume", model);
            }

            else if (command == "submit")
            {
                ResumeJsonDto result = await _service.ParseResumeAsync(model);

                Resume resume = MapToResumeEntity(result, User.FindFirstValue(ClaimTypes.NameIdentifier));

                _repo.AddResume(resume);

                return RedirectToAction("Index");
            }

            return View("CreateNewResume", model);


        }

      
            };

        }


  