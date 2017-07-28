using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloBooks.Models;
using HelloBooks.Utilities;

namespace HelloBooks.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.User);
            return View(books.ToList());
        }

        // GET: Books/Details/5
     //   public ActionResult Details(int? id)
     //   {
     //       if (id == null)
     //       {
     //           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
     //       }
     //       Book book = db.Books.Find(id);
     //       if (book == null)
     //       {
     //           return HttpNotFound();
     //       }
     //       return View(book);
     //   }

     //   // GET: Books/Create
     //   public ActionResult Create()
     //   {
     //       ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
     //       return View();
     //   }

	    //public ActionResult List()
	    //{
		   // return View();
	    //}
     //   // POST: Books/Create
     //   // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
     //   // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
     //   [HttpPost]
     //   [ValidateAntiForgeryToken]
     //   public ActionResult Create([Bind(Include = "Id,ApplicationUserId,Title,Isbn,TotalPageCount,DoneWithBook")] Book book)
     //   {
     //       if (ModelState.IsValid)
     //       {
     //           db.Books.Add(book);
     //           db.SaveChanges();
     //           return RedirectToAction("Index");
     //       }

     //       ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
     //       return View(book);
     //   }

     //   // GET: Books/Edit/5
     //   public ActionResult Edit(int? id)
     //   {
     //       if (id == null)
     //       {
     //           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
     //       }
     //       Book book = db.Books.Find(id);
     //       if (book == null)
     //       {
     //           return HttpNotFound();
     //       }
     //       ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
     //       return View(book);
     //   }

     //   // POST: Books/Edit/5
     //   // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
     //   // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
     //   [HttpPost]
     //   [ValidateAntiForgeryToken]
     //   public ActionResult Edit([Bind(Include = "Id,ApplicationUserId,Title,Isbn,TotalPageCount,DoneWithBook")] Book book)
     //   {
     //       if (ModelState.IsValid)
     //       {
     //           db.Entry(book).State = EntityState.Modified;
     //           db.SaveChanges();
     //           return RedirectToAction("Index");
     //       }
     //       ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "FirstName", book.ApplicationUserId);
     //       return View(book);
     //   }

     //   // GET: Books/Delete/5
     //   public ActionResult Delete(int? id)
     //   {
     //       if (id == null)
     //       {
     //           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
     //       }
     //       Book book = db.Books.Find(id);
     //       if (book == null)
     //       {
     //           return HttpNotFound();
     //       }
     //       return View(book);
     //   }

     //   // POST: Books/Delete/5
     //   [HttpPost, ActionName("Delete")]
     //   [ValidateAntiForgeryToken]
     //   public ActionResult DeleteConfirmed(int id)
     //   {
     //       Book book = db.Books.Find(id);
     //       db.Books.Remove(book);
     //       db.SaveChanges();
     //       return RedirectToAction("Index");
     //   }

     //   protected override void Dispose(bool disposing)
     //   {
     //       if (disposing)
     //       {
     //           db.Dispose();
     //       }
     //       base.Dispose(disposing);
     //   }
    }
}
