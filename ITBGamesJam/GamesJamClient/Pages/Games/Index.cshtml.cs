using GamesJamClient.Models;
using GamesJamClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamesJamClient.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Game> Games { get; set; }

        public async Task OnGetAsync()
        {
            Games = await _apiService.GetTopGamesAsync();
        }

        public async Task<IActionResult> OnPostVoteAsync(int gameId)
        {
            var success = await _apiService.VoteForGameAsync(gameId);
            return success ? RedirectToPage() : BadRequest();
        }
    }
}
