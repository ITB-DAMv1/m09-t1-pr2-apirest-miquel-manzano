namespace GamesJamApi.DTO
{
    public class VoteResponseDto
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; }
        public int TotalVotes { get; set; }
        public bool HasVoted { get; set; }
    }
}
