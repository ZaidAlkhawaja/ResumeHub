using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ResumeHub.DTOs;
using ResumeHub.Models;
using ResumeHub.Interfaces;

namespace ResumeHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IReviewRepo _reviews;
   

    public HomeController(ILogger<HomeController> logger, IReviewRepo reviewRepo)
    {
        _logger = logger;
        _reviews = reviewRepo; 
    }

    public IActionResult Index()
    {
         var r = _reviews.GetRecentAsync();
        ViewBag.RecentReviews = r ;
        return View( new ReviewDto());
    }

    [Authorize]

    [HttpPost]

    public async Task<IActionResult> SubmitReview(ReviewDto dto)

    {

        if (!ModelState.IsValid)

        {

            return View("Index", dto);

        }
        else

        {

            var review = new Review

            {

                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),

                UserName = User.Identity.Name,

                Rating = dto.Rating,

                Comment = dto.Comment,

                CreatedAt = DateTime.UtcNow

            };

            await _reviews.AddAsync(review);

            TempData["SuccessMessage"] = "Review submitted successfully!";

            return RedirectToAction("Index");

        }

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
