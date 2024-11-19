using BlogApp.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthenticationController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRegister(RegisterDto model)
        {
            if(ModelState.IsValid)
            {
                var registerdto = new RegisterDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,

                };

                context.Register.Add(registerdto);
                context.SaveChanges();

                return RedirectToAction("Index","Authentication");
            }

            return RedirectToAction("Index", "Authentication");
        }
    }
}
