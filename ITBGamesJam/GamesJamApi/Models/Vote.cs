using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamesJamApi.Models
{
    [Table("Votes")]
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Game")]  // ← Esto indica que GameId es FK a la propiedad de navegación Game
        public int GameId { get; set; }

        [ForeignKey("User")]  // ← Esto indica que UserId es FK a la propiedad de navegación User
        public string UserId { get; set; }

        public DateTime VoteDate { get; set; } = DateTime.UtcNow;

        // Propiedades de navegación
        public virtual Game Game { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
