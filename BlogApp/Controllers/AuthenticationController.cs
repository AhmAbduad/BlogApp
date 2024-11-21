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
        public IActionResult Register(RegisterDto model)
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
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if(ModelState.IsValid)
            {
                var user = context.Register.SingleOrDefault(u=>u.Email == login.Email);

                if(user==null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return View(login);
                }

                
                if (user.Password != login.Password)
                {
                    ModelState.AddModelError("Password", "Incorrect password.");
                    return View(login);
                }
            }

            return RedirectToAction("Index", "Authentication");
        }
    }
}
