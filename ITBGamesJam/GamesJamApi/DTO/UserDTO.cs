using System.ComponentModel.DataAnnotations;

namespace GamesJamApi.DTO
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
