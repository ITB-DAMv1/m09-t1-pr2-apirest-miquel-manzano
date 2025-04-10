using System.ComponentModel.DataAnnotations;

namespace GamesJamApi.DTO
{
    public class GameDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
