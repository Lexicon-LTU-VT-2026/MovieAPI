// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace MovieAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
