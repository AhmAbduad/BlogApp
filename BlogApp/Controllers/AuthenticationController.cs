using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
