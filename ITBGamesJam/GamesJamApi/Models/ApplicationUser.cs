using Microsoft.AspNetCore.Identity;

namespace GamesJamApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
