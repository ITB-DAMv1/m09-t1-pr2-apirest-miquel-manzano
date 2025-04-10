using GamesJamApi.Context;
using GamesJamApi.DTO;
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

        [HttpGet("All Games")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetFilms()
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
    }
}
