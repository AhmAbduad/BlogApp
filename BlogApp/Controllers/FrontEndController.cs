using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class FrontEndController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
