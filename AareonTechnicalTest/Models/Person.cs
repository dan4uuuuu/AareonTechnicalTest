using Microsoft.AspNetCore.Identity;

namespace AareonTechnicalTest.Models
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }
}
