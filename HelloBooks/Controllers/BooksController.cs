using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;

namespace HelloBooks.Controllers
{
	[Authorize]
	public class BooksController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Books
		public ActionResult Index()
		{
			//var books = db.Books.Include(b => b.User);
			var userId = User.Identity.GetUserId();
			ICollection<Book> books = db.Books.Where(c => c.ApplicationUserId == userId).ToList();

			return View(books);
		}

		// GET: Books/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Book book = db.Books.Find(id);
			if (book == null)
			{
				return HttpNotFound();
			}
			var userId = User.Identity.GetUserId();
			if (book.User.Id != userId)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

			}
			return View(book);
		}

		// GET: Books/Create
		public ActionResult Create()
		{
			return View();
		}


		// POST: Books/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Title,Isbn,TotalPageCount")] Book book)
		{
			var userId = User.Identity.GetUserId();
			book.ApplicationUserId = userId;
			//new ApplicationDbContext().Users.First(c => c.Id == userId);
			book.DoneWithBook = false;
			//if (!ModelState.IsValid) return View(book);
			db.Books.Add(book);
			db.SaveChanges();
			return RedirectToAction("Index");

			//ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ISBNSearch(string autoISBN)
		{
			IsbnLookup lookup = new IsbnLookup(autoISBN);
			if (lookup.ReturnedOneVolume)
			{

				ViewBag.ResultTitle = lookup.Title;
				ViewBag.ResultISBN = lookup.ResultIsbn;
				ViewBag.ResultPageCount = lookup.PageCount;
				ViewBag.ThumbnailLink = lookup.ThumbnailLink;
				ViewBag.ReturnIsbn = "";
				
			}
			else
			{
				ViewBag.ResultTitle = string.Empty;
				ViewBag.ResultISBN = string.Empty;
				ViewBag.ResultPageCount = string.Empty;
				ViewBag.ReturnIsbn = "No results found for the ISBN: " + lookup.ReturnIsbn;

			}
			return View("Create");
		}

		// GET: Books/Edit/5
		//public ActionResult Edit(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Book book = db.Books.Find(id);
		//	if (book == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
		//	return View(book);
		//}

		// POST: Books/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Id,ApplicationUserId,Title,Isbn,TotalPageCount,DoneWithBook")] Book book)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Entry(book).State = EntityState.Modified;
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		//	ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
		//	return View(book);
		//}

		// GET: Books/Delete/5
		//public ActionResult Delete(int? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Book book = db.Books.Find(id);
		//	if (book == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(book);
		//}

		//// POST: Books/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(int id)
		//{
		//	Book book = db.Books.Find(id);
		//	db.Books.Remove(book);
		//	db.SaveChanges();
		//	return RedirectToAction("Index");
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}
	}
}
