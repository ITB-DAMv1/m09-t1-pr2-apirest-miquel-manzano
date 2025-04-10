using Microsoft.AspNetCore.Mvc;

namespace GamesJamApi.Controllers
{
    public class VotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
