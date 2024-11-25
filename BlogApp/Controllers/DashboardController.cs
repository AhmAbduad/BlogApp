using BlogApp.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BlogApp.Controllers
{
	public class DashboardController : Controller
	{
		private readonly ApplicationDbContext context;

		public DashboardController(ApplicationDbContext context) 
		{
			this.context = context;
		}

		public IActionResult Index(int id)
		{
			ViewData["Id"] = id;
			return View();
		}

		public IActionResult Profile(int id)
		{
			// getting all the data of the user from register table
			var user = context.Register.FirstOrDefault(x => x.Id == id);

			if(user == null)
			{
				return NotFound();
			}

			ViewData["Id"] = user.Id;
			ViewData["UserName"] = user.UserName;
			ViewData["FirstName"] = user.FirstName;
			ViewData["LastName"] = user.LastName;
			ViewData["Email"] = user.Email;
			ViewData["Password"] = user.Password;


			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Updatingprofile(RegisterDto model)
		{
			var user = context.Register.FirstOrDefault(p=>p.Id == model.Id);

			if(user == null)
			{
				return NotFound();
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.UserName = model.UserName;
			user.Email = model.Email;
			user.Password = model.Password;

			context.SaveChanges();

			return RedirectToAction("Index", "Dashboard", new { id = user.Id });
		}
	}
}
