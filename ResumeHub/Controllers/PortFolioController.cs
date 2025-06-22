using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ResumeHub.DTOs;
using ResumeHub.Models;
using ResumeHub.DTOs;
using ResumeHub.Interfaces;
using ResumeHub.Models;
using ResumeHub.Repositories;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;

namespace ResumeHub.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ILogger<PortfolioController> _logger;

        private readonly IPortfolioOpenAiService _portfolioOpenAiService;
        private readonly IPortFolioRepository _portfolioRepo;
        private readonly EmailSettings _emailSettings;


        public PortfolioController(IPortFolioRepository portfolioRepo, IPortfolioOpenAiService portfolioOpenAiService, IOptions<EmailSettings> emailSettings)
        {

            _portfolioRepo = portfolioRepo;
            _portfolioOpenAiService = portfolioOpenAiService;
            _emailSettings = emailSettings.Value;
        }

       
        public IActionResult PortFolioPage()
        {
            
            return View();
        
        }
        public async Task<IActionResult> PortFoliosTemplates(int ID)
        {
            var resume = await _portfolioRepo.GetPortFolioById(ID);
            if (resume == null)
            {
                return NotFound();
            }
            var dto = MapToPortfolioJsonDto(resume);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePortFolio(int ID)
        {
            await _portfolioRepo.DeletePortFolio(ID);

            return RedirectToAction("Index");
        }

        public async  Task <IActionResult> Edit(int Id)
        {
            // Fetch the portfolio by Id
            var portfolio = await _portfolioRepo.GetPortFolioById(Id);
            if (portfolio == null)
            {
                return NotFound();
            }
            // Convert the portfolio to JSON DTO
            var portfolioDto = ConvertPortFolioToJsonDtos(new List<PortFolio> { portfolio }).FirstOrDefault();
            if (portfolioDto == null)
            {
                return NotFound();
            }
            return View(portfolioDto);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(PortfolioJsonDto dto)
        {
            // Fetch the portfolio by Id
            if (!ModelState.IsValid)
            {
                return View("Edit", dto);
            }
            else
            {
                if (dto.PersonalImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await dto.PersonalImage.CopyToAsync(ms);
                        var bytes = ms.ToArray();
                        dto.ImageBase64 = Convert.ToBase64String(bytes);
                        dto.ImageFileName = dto.PersonalImage.FileName;
                        dto.ImageContentType = dto.PersonalImage.ContentType;
                    }
                }

                if(dto.Projects != null && dto.Projects.Count > 0)
                {
                    foreach (var project in dto.Projects)
                    {
                        if (project.ProjectImage != null)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await project.ProjectImage.CopyToAsync(ms);
                                var bytes = ms.ToArray();
                                project.ImageBase64 = Convert.ToBase64String(bytes);
                                project.ImageFileName = project.ProjectImage.FileName;
                                project.ImageContentType = project.ProjectImage.ContentType;
                            }
                        }
                    }
                }

                    var p = MapToPortFolioEntity(dto, User.FindFirstValue(ClaimTypes.NameIdentifier));
                    p.PortFolioId = dto.Id;
                    await _portfolioRepo.UpdatePortFolio(p);
                    return RedirectToAction("Index");
                }
            }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Fetch resumes for the current user
            var portFolios = _portfolioRepo.GetPortFolioByEndUserId(userId);
            // Convert resumes to JSON DTOs
            var portfolioDtos = ConvertPortFolioToJsonDtos(portFolios);
            return View(portfolioDtos);
        }



        private List<PortfolioJsonDto> ConvertPortFolioToJsonDtos(List<PortFolio> portfolios)
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
                GitHubLink = portfolio.GitHubProfile ,
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
                    ProjectLink = proj.ProjectUrl , 
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

        [HttpGet]
        public IActionResult CreateNewPortFolio()
        {
            var dto = new PortfolioDTO
            {
                CurrentStep = 1
            };
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessStep(PortfolioDTO model, string command)
        {
            // === BACK button clicked ===
            if (command == "prev")
            {
                if (model.CurrentStep > 1)
                {
                    model.CurrentStep--;
                }
                return View("CreateNewPortFolio", model);
            }

            // === NEXT button clicked ===
            if (command == "next")
            {
                // Handle profile image upload and convert to base64 on step 1
                if (model.CurrentStep == 1 && model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ProfileImage.CopyToAsync(ms);
                        var bytes = ms.ToArray();
                        model.ProfileImageBase64 = Convert.ToBase64String(bytes);
                        model.ProfileImageFileName = model.ProfileImage.FileName;
                        model.ProfileImageContentType = model.ProfileImage.ContentType;
                    }
                    model.ProfileImage = null; // Clear the file to avoid model binding issues
                }
                // If no new image was uploaded but we have existing base64 data, retain it
                else if (model.CurrentStep == 1 && model.ProfileImage == null && !string.IsNullOrEmpty(model.ProfileImageBase64))
                {
                    // Keep the existing image data
                }

                // Handle project images (if any) and convert to base64 on step 2
                if (model.CurrentStep == 2 && model.Projects != null)
                {
                    for (int i = 0; i < model.Projects.Count; i++)
                    {
                        var project = model.Projects[i];
                        if (project.Image != null && project.Image.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await project.Image.CopyToAsync(ms);
                                var bytes = ms.ToArray();
                                project.ImageBase64 = Convert.ToBase64String(bytes);
                                project.ImageFileName = project.Image.FileName;
                                project.ImageContentType = project.Image.ContentType;
                            }
                            project.Image = null; // Clear the file to avoid model binding issues
                        }
                        // If no new image was uploaded but we have existing base64 data, retain it
                        else if (project.Image == null && !string.IsNullOrEmpty(project.ImageBase64))
                        {
                            // Keep the existing image data
                        }
                    }
                }

                // Rest of the validation logic remains the same...
                // [Previous validation code here]

                if (model.CurrentStep < 3)
                {
                    model.CurrentStep++;
                }
                return View("CreateNewPortFolio", model);
            }

          
            if (command == "submit" && model.CurrentStep == 3)
            {
                PortfolioJsonDto result = await _portfolioOpenAiService.ParsePortfolioDataAsync(model);

                if (result == null)
                {
                    ModelState.AddModelError("", "Failed to generate portfolio. Please try again.");
                    return View("CreateNewPortFolio", model);
                }

                PortFolio portFolio = MapToPortFolioEntity (result, User.FindFirstValue(ClaimTypes.NameIdentifier));
                portFolio.ImageContentType = model.ProfileImageContentType;
                portFolio.ImageBase64 = model.ProfileImageBase64;
                portFolio.ImageFileName = model.ProfileImageFileName;
                for(var i =0; i < model.Projects.Count; i++)
                {
                    portFolio.Projects[i].ImageBase64 = model.Projects[i].ImageBase64;
                    portFolio.Projects[i].ImageContentType = model.Projects[i].ImageContentType;
                    portFolio.Projects[i].ImageFileName = model.Projects[i].ImageFileName;
                }

                  _portfolioRepo.AddPortFolio(portFolio);
               
                return RedirectToAction("Index");
         
            }

            // FALLBACK: stay on the same step
            return View("CreateNewPortFolio", model);
        }

        private PortfolioJsonDto MapToPortfolioJsonDto(PortFolio portfolio)
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
        private PortFolio MapToPortFolioEntity(PortfolioJsonDto dto, string userId)
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

        [HttpGet]

        public IActionResult PortFolioTemplate1(PortfolioJsonDto dto)
        {
            return View(dto);
    
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(int portfolioId, string name, string email, string subject, string message)
        {
            // 1. Look up the portfolio
            var portfolio = await _portfolioRepo.GetPortFolioById(portfolioId);
            if (portfolio == null)
                return NotFound();

            // 2. Build the email with cleaner formatting
            var mime = new MimeMessage();
            mime.From.Add(new MailboxAddress(
                _emailSettings.FromName,
                _emailSettings.FromAddress));
            mime.To.Add(new MailboxAddress(
                $"{portfolio.firstName} {portfolio.lastName}",
                portfolio.Email));
            mime.Subject = subject;

            // Clean, professional message formatting
            var body = new BodyBuilder
            {
                TextBody = $"Contact Form Submission\n" +
                          $"-----------------------\n" +
                          $"Name: {name}\n" +
                          $"Email: {email}\n\n" +
                          $"Message:\n" +
                          $"{message.Trim()}\n\n" +
                          $"-----------------------\n" +
                          $"Sent from your portfolio contact form"
            };
            mime.Body = body.ToMessageBody();

            // 3. Send via SMTP (keep your working code)
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(
                _emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                _emailSettings.Username,
                _emailSettings.Password);
            await client.SendAsync(mime);
            await client.DisconnectAsync(true);

            // 4. Confirm to user
            TempData["ContactSuccess"] = "Your message has been sent!";
            return RedirectToAction("PortFoliosTemplates", new { ID = portfolioId });
        }
    }

}
