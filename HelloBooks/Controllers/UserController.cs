using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Calendar.v3.Data;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HelloBooks.Controllers
{
	public class UserController : Controller
	{
		private IApplicationDbContext db;
		private IPrincipal principal;
		private ApplicationUserManager _userManager;
		private ApplicationSignInManager _signInManager;

		public UserController()
		{
			db = new ApplicationDbContext();
			principal = System.Web.HttpContext.Current.User;
		}

		public UserController(IApplicationDbContext dbContext)
		{
			db = dbContext;
			principal = System.Web.HttpContext.Current.User;
		}

		public UserController(IApplicationDbContext dbContext, IPrincipal principal)
		{
			db = dbContext;
			this.principal = principal;
		}
		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}
		// GET: User
		public async Task<ActionResult> Settings()
		{
			var user = await UserManager.FindByIdAsync(principal.Identity.GetUserId());
			if (user != null)
			{
				await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
			}

			return View(user);
		}

		public ActionResult GetIdentityFacts()
		{
			var googleId = UserManager.GetLogins(principal.Identity.GetUserId());
			var id = googleId.First(c => c.LoginProvider == "Google").ProviderKey;
			ViewBag.GoogleProviderKey = id;
			return View(googleId);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NewReadingDifficulty(
			[Bind(Include = "DifficultyTitle,ApplicationUserId,PagesPerHour")] ReadingDifficulty newDifficulty)
		{
			//ApplicationUser user = UserManager.FindById(newDifficulty.ApplicationUserId);
			//newDifficulty.User = user;
			//user.UserDefinedReadingDifficulties.Add(newDifficulty);



			db.ReadingDifficulties.Add(newDifficulty);
			db.SaveChanges();
			return RedirectToAction("Settings", "User");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NewReadingCalendar()
		{
			//Calendar newCalendar = new Calendar()
			//{
			//	Description = "The HelloBooks.io reading calendar where availability can be established by creating reading events. Because this calendar is exclusive to HelloBooks.io any event created on this calendar will be treated as reading availability.",
			//	Summary = "HelloBooks.io Reading Calendar"
			//};
			//Calendar createdCalendar = new UserCalendarService().Service.Calendars.Insert(newCalendar).Execute();


			ApplicationUser user = UserManager.FindById(principal.Identity.GetUserId());

			user.GoogleCalendarId = new UserCalendarService(user).CreateNewCalendarWithId();
			user.GoogleCalendarIsUnique = true;
			UserManager.Update(user);
			return RedirectToAction("Settings");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UseExistingCalendar([Bind(Include = "Id,GoogleCalendarId")]ApplicationUser userModel)
		{
			ApplicationUser user = UserManager.FindById(userModel.Id);
			user.GoogleCalendarId = userModel.GoogleCalendarId;
			user.GoogleCalendarIsUnique = false;
			UserManager.Update(user);
			return RedirectToAction("Settings");
		}

	}
}