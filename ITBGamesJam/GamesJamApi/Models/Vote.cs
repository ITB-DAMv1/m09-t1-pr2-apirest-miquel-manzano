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

        public Game Game { get; set; }
        public ApplicationUser User {  get; set; }
    }
}
