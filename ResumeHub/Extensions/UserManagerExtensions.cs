using Microsoft.AspNetCore.Identity;
using ResumeHub.Models;

namespace ResumeHub.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<List<Person>> GetLastFiveEndUsersAsync(this UserManager<Person> userManager)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync("EndUser");
            return usersInRole
                .OrderByDescending(u => u.Id)
                .Take(5)
                .ToList();
        }
    }
}
