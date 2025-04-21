using Microsoft.AspNetCore.Identity;

namespace GamesJamApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        // Propiedad de navegación para los votos del usuario
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
