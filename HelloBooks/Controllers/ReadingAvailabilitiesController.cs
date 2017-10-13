using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HelloBooks.Controllers
{
	public class ReadingAvailabilitiesController : Controller
	{
		private IApplicationDbContext db;

		private IPrincipal principal;


		public ReadingAvailabilitiesController()
		{
			db = new ApplicationDbContext();
			principal = System.Web.HttpContext.Current.User;
		}

		public ReadingAvailabilitiesController(IApplicationDbContext dbContext)
		{
			db = dbContext;
			principal = System.Web.HttpContext.Current.User;
		}

		public ReadingAvailabilitiesController(IApplicationDbContext dbContext, IPrincipal principal)
		{
			db = dbContext;
			this.principal = principal;
		}

		// GET: ReadingAvailabilities
		public ActionResult Index()
		{
			ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>()
				.FindById(principal.Identity.GetUserId());

			UserCalendarService calendarService = new UserCalendarService(user);

			calendarService.SyncEventsFromGoogle();
			var readingAvailabilities = db.ReadingAvailabilities.Include(r => r.User);
			return View(readingAvailabilities.ToList());
		}

		// GET: ReadingAvailabilities/Details/5
//		public ActionResult Details(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			ReadingAvailability readingAvailability = db.ReadingAvailabilities.Find(id);
//			if (readingAvailability == null)
//			{
//				return HttpNotFound();
//			}
//			return View(readingAvailability);
//		}

//		// GET: ReadingAvailabilities/Create
//		public ActionResult Create()
//		{
//			ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
//			return View();
//		}

//		// POST: ReadingAvailabilities/Create
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public ActionResult Create([Bind(Include = "Id,ReadingStartTime,ReadingEndTime,ApplicationUserId,CalenderEventId")] ReadingAvailability readingAvailability)
//		{
//			if (ModelState.IsValid)
//			{
//				db.ReadingAvailabilities.Add(readingAvailability);
//				db.SaveChanges();
//				return RedirectToAction("Index");
//			}

//			ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", readingAvailability.ApplicationUserId);
//			return View(readingAvailability);
//		}

//		// GET: ReadingAvailabilities/Edit/5
//		public ActionResult Edit(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			ReadingAvailability readingAvailability = db.ReadingAvailabilities.Find(id);
//			if (readingAvailability == null)
//			{
//				return HttpNotFound();
//			}
//			ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", readingAvailability.ApplicationUserId);
//			return View(readingAvailability);
//		}

//		// POST: ReadingAvailabilities/Edit/5
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public ActionResult Edit([Bind(Include = "Id,ReadingStartTime,ReadingEndTime,ApplicationUserId,CalenderEventId")] ReadingAvailability readingAvailability)
//		{
//			if (ModelState.IsValid)
//			{
//				db.Entry(readingAvailability).State = EntityState.Modified;
//				db.SaveChanges();
//				return RedirectToAction("Index");
//			}
//			ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", readingAvailability.ApplicationUserId);
//			return View(readingAvailability);
//		}

//		// GET: ReadingAvailabilities/Delete/5
//		public ActionResult Delete(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			ReadingAvailability readingAvailability = db.ReadingAvailabilities.Find(id);
//			if (readingAvailability == null)
//			{
//				return HttpNotFound();
//			}
//			return View(readingAvailability);
//		}

//		// POST: ReadingAvailabilities/Delete/5
//		[HttpPost, ActionName("Delete")]
//		[ValidateAntiForgeryToken]
//		public ActionResult DeleteConfirmed(int id)
//		{
//			ReadingAvailability readingAvailability = db.ReadingAvailabilities.Find(id);
//			db.ReadingAvailabilities.Remove(readingAvailability);
//			db.SaveChanges();
//			return RedirectToAction("Index");
//		}

//		protected override void Dispose(bool disposing)
//		{
//			if (disposing)
//			{
//				db.Dispose();
//			}
//			base.Dispose(disposing);
//		}
	}
}
