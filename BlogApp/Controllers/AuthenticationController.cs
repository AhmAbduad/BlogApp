using BlogApp.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext context;
        private const int MaxFailedAttempts = 3; // Max failed login attempts before lockout
        private const int LockoutDurationInMinutes = 5; // Lockout duration in minutes


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
           
                var user = context.Register.SingleOrDefault(u => u.Email == login.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return View(login);
                }

                var loginAttempt = context.LoginAttempts.FirstOrDefault(la => la.Username == login.Email);

                if (loginAttempt == null)
                {
                    loginAttempt = new LoginAttempt()
                    {
                        Username = login.Email,
                        FailedAttempts = 0,
                        LastFailedAttempt = DateTime.Now
                    };

                    context.LoginAttempts.Add(loginAttempt);
                    context.SaveChanges();
                }


                if (loginAttempt.FailedAttempts >= MaxFailedAttempts)
                {
                    if ((DateTime.Now - loginAttempt.LastFailedAttempt).TotalMinutes < LockoutDurationInMinutes)
                    {
                        ViewBag.Error = "Your account is locked. Please try again after 5 minutes.";
                        return View();
                    }
                    else
                    {
                        // reset failed attempts if the login time hass passed out
                        loginAttempt.FailedAttempts = 0;
                        context.LoginAttempts.Update(loginAttempt);
                        context.SaveChanges();
                    }
                }

                if (user.Password == login.Password)
                {
                    loginAttempt.FailedAttempts = 0;
                    context.LoginAttempts.Update(loginAttempt);
                    context.SaveChanges();

					return RedirectToAction("Index", "Dashboard", new { id = user.Id });
				}
                else
                {
                    ModelState.AddModelError("Password", "Incorrect password.");
                    


					loginAttempt.FailedAttempts++;
					loginAttempt.LastFailedAttempt = DateTime.Now;
					context.LoginAttempts.Update(loginAttempt);
					context.SaveChanges();

					return View(login);
				}
		}
    }
}
