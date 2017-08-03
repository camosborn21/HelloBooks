using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;
using System.Web.Routing;

namespace HelloBooks.Controllers
{
	[Authorize]
	public class BooksController : Controller
	{
		private IApplicationDbContext db;
		private IPrincipal principal;

		//protected override void Initialize(RequestContext reqeustContext)
		//{
		//	base.Initialize(reqeustContext);
		//}
		
		public BooksController()
		{
			db = new ApplicationDbContext();
			principal = System.Web.HttpContext.Current.User;
		}

		public BooksController(IApplicationDbContext dbContext)
		{
			db = dbContext;
			principal = System.Web.HttpContext.Current.User;
		}

		public BooksController(IApplicationDbContext dbContext, IPrincipal principal)
		{
			db = dbContext;
			this.principal = principal;
		}

		// GET: Books
		public ActionResult Index()
		{
			
			var userId = principal.Identity.GetUserId();
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
			var userId = principal.Identity.GetUserId();
			if (book.User.Id != userId)
			{
				return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

			}
			return View(book);
		}

		// GET: Books/Edit/5
		public ActionResult Edit(int? id)
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
			return View(book);
		}

		// GET: Books/Create
		public ActionResult Create()
		{
			Book m = new Book();
			return View(m);
		}


		// POST: Books/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ThumbnailLink,Title,Isbn,TotalPageCount")] Book book)
		{
			var userId = principal.Identity.GetUserId();
			book.ApplicationUserId = userId;
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
			Book m = new Book();
			
			if (lookup.ReturnedOneVolume)
			{


				m.Title = lookup.Title;
				m.Isbn = lookup.ResultIsbn;
				m.TotalPageCount = lookup.PageCount ?? 0;
				m.ThumbnailLink = lookup.ThumbnailLink;
				ViewBag.ReturnIsbn = "";

			}
			else
			{
				ViewBag.ResultTitle = string.Empty;
				ViewBag.ResultISBN = string.Empty;
				ViewBag.ResultPageCount = string.Empty;
				ViewBag.ReturnIsbn = "No results found for the ISBN: " + lookup.ReturnIsbn;

			}
			return View("Create", m);
		}



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
