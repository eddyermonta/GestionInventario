
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.src.Modules.Users.Domains.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public static string GetFullName(ApplicationUser user) => $"{user.FirstName} {user.LastName}";
        
    }
}