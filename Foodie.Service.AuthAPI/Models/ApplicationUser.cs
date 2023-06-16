using Microsoft.AspNetCore.Identity;

namespace Foodie.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
