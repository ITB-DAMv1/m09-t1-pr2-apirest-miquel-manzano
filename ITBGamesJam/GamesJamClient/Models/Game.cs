namespace GamesJamClient.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string TeamName { get; set; }
        public string Description { get; set; }
        public int VotesCount { get; set; }
        public bool HasVoted { get; set; }
        //public string ImageUrl { get; set; }
    }
}
