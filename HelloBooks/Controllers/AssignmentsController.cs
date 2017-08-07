using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace HelloBooks.Controllers
{
	public class AssignmentsController : Controller
	{
		private IApplicationDbContext db;
		private IPrincipal principal;

		public AssignmentsController()
		{
			db = new ApplicationDbContext();
			principal = System.Web.HttpContext.Current.User;
		}

		public AssignmentsController(IApplicationDbContext dbContext)
		{
			db = dbContext;
			principal = System.Web.HttpContext.Current.User;
		}

		public AssignmentsController(IApplicationDbContext dbContext, IPrincipal principal)
		{
			db = dbContext;
			this.principal = principal;
		}





		//[HttpGet]
		//public ActionResult AddNewAssignment(int? bookId)
		//{
		//	Book book = db.Books.First(c => c.Id == bookId);
		//	Assignment newAssignment = new Assignment()
		//	{
		//		Book = book
		//	};
		//	return PartialView("_newAssignmentPartial");
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NewAssignment([Bind(Include = "AssignmentName,DueDate,BookId,ReadingDifficultyId")]Assignment newAssignment)
		{
			
			Book book = db.Books.First(c => c.Id == newAssignment.BookId);
			
			newAssignment.Book = book;
			ReadingDifficulty readingDifficulty = db.ReadingDifficulties.First(c => c.Id == newAssignment.ReadingDifficultyId);
			
			book.Assignments.Add(newAssignment);
			db.SaveChanges();
			return RedirectToAction("Details", "Books", new {id = newAssignment.BookId});
			
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NewRequiredReading([Bind(Include = "StartPage,LastPage,AssignmentId")]RequiredPages newReading)
		{
			Assignment assignment = db.Assignments.First(c => c.Id == newReading.AssignmentId);
			newReading.Assignment = assignment;
			assignment.Pages.Add(newReading);
			db.SaveChanges();
			return RedirectToAction("Details", "Books", new {id = assignment.BookId});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult NewReadingProgress(
			[Bind(Include = "AssignmentId,StartPage,FinishPage,StartDateTime,EndDateTime")] ReadingProgress newProgress)
		{
			Assignment assignment = db.Assignments.First(c => c.Id == newProgress.AssignmentId);
			newProgress.Assignment = assignment;
			assignment.Reading.Add(newProgress);
			db.SaveChanges();
			return RedirectToAction("Details", "Books", new {id = assignment.BookId});

		}

		//// GET: Assignments
		//public ActionResult Index()
		//{
		//    return View(db.Assignments.ToList());
		//}

		//// GET: Assignments/Details/5
		//public ActionResult Details(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Assignment assignment = db.Assignments.Find(id);
		//    if (assignment == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(assignment);
		//}

		//// GET: Assignments/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		//// POST: Assignments/Create
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create([Bind(Include = "Id,AssignmentName,DueDate,ModifiedPagesPerHour")] Assignment assignment)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        db.Assignments.Add(assignment);
		//        db.SaveChanges();
		//        return RedirectToAction("Index");
		//    }

		//    return View(assignment);
		//}

		//// GET: Assignments/Edit/5
		//public ActionResult Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Assignment assignment = db.Assignments.Find(id);
		//    if (assignment == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(assignment);
		//}

		//// POST: Assignments/Edit/5
		//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Id,AssignmentName,DueDate,ModifiedPagesPerHour")] Assignment assignment)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        db.Entry(assignment).State = EntityState.Modified;
		//        db.SaveChanges();
		//        return RedirectToAction("Index");
		//    }
		//    return View(assignment);
		//}

		//// GET: Assignments/Delete/5
		//public ActionResult Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Assignment assignment = db.Assignments.Find(id);
		//    if (assignment == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(assignment);
		//}

		//// POST: Assignments/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(int id)
		//{
		//    Assignment assignment = db.Assignments.Find(id);
		//    db.Assignments.Remove(assignment);
		//    db.SaveChanges();
		//    return RedirectToAction("Index");
		//}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}
	}
}
