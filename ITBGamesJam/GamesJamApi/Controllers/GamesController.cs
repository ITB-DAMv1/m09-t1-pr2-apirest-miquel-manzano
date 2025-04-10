using Microsoft.AspNetCore.Mvc;

namespace GamesJamApi.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
