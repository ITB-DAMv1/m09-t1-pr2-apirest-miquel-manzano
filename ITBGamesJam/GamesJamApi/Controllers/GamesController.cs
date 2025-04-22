using GamesJamApi.Context;
using GamesJamApi.DTO;
using GamesJamApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesJamApi.Controllers
{
    [Route("/api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // Injeccio DbContext
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("TestApi")]
        public IActionResult helloClient()
        {
            return Ok("Helo client");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _context.Games
                .Select(g => new GameDTO
                {
                    Name = g.Name,
                    Description = g.Description,
                })
                .ToListAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if(game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        //[Authorize]
        [HttpPost("NewGame")]
        public async Task<ActionResult<Game>> PostGame(GameDTO gameDTO)
        {
            var game = new Game
            {
                Name = gameDTO.Name,
                Description = gameDTO.Description
            };

            try
            {
                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }


            // return CreatedAtAction(nameof(GetFilm), new { id = film.ID }, film);
            return Ok(game);
        }

        //[Authorize]
        [HttpDelete("DeleteGame/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            try
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        //[Authorize]
        [HttpPut("EditGame/{id}")]
        public async Task<IActionResult> PutFilm(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(game);
        }


        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
