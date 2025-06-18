using Microsoft.AspNetCore.Identity;

namespace ResumeHub.Models
{
    public class Person : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

       

        public string Address { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }
}
