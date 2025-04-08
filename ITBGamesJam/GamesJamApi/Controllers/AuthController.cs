using Microsoft.AspNetCore.Mvc;

namespace GamesJamApi.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
