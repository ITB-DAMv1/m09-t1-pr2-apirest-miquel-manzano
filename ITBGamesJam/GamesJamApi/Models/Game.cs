using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamesJamApi.Models
{
    [Table("Games")]
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

        // Propiedad de navegación para los votos
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
