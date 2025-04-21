using GamesJamApi.Context;
using GamesJamApi.DTO;
using GamesJamApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesJamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VotesController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/Votes
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<VoteResponseDto>> PostVote(VoteDto voteDto)
        {
            // Obtener el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Verificar si el juego existe
            var game = await _context.Games.FindAsync(voteDto.GameId);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            // Verificar si el usuario ya ha votado este juego
            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.GameId == voteDto.GameId && v.UserId == user.Id);

            if (existingVote != null)
            {
                return BadRequest("You have already voted for this game");
            }

            // Crear nuevo voto
            var vote = new Vote
            {
                GameId = voteDto.GameId,
                UserId = user.Id
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            // Obtener el conteo actualizado de votos
            var totalVotes = await _context.Votes.CountAsync(v => v.GameId == voteDto.GameId);

            var response = new VoteResponseDto
            {
                GameId = game.Id,
                GameTitle = game.Name,
                TotalVotes = totalVotes,
                HasVoted = true
            };

            return Ok(response);
        }

        // GET: api/Votes/Game/5
        [HttpGet("Game/{gameId}")]
        [AllowAnonymous]
        public async Task<ActionResult<VoteResponseDto>> GetVotesForGame(int gameId)
        {
            // Verificar si el juego existe
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            // Obtener conteo de votos
            var totalVotes = await _context.Votes.CountAsync(v => v.GameId == gameId);

            // Verificar si el usuario actual ha votado (si está autenticado)
            bool hasVoted = false;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                hasVoted = await _context.Votes
                    .AnyAsync(v => v.GameId == gameId && v.UserId == user.Id);
            }

            var response = new VoteResponseDto
            {
                GameId = game.Id,
                GameTitle = game.Name,
                TotalVotes = totalVotes,
                HasVoted = hasVoted
            };

            return Ok(response);
        }

        // GET: api/Votes/User
        [HttpGet("User")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<VoteResponseDto>>> GetVotesByUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var votes = await _context.Votes
                .Where(v => v.UserId == user.Id)
                .Include(v => v.Game)
                .Select(v => new VoteResponseDto
                {
                    GameId = v.GameId,
                    GameTitle = v.Game.Name,
                    TotalVotes = _context.Votes.Count(v2 => v2.GameId == v.GameId),
                    HasVoted = true
                })
                .ToListAsync();

            return Ok(votes);
        }
    }
}
