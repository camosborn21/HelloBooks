using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloBooks;
using HelloBooks.Controllers;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Moq;

namespace HelloBooks.Tests.Controllers
{
	[TestClass]
	public class BooksControllerTest
	{
		[TestMethod]
		public void BooksIndexAlwaysReturnsLibrary()
		{
			// Arrange
			BooksController controller = new BooksController();
			var mock = new Mock<ControllerContext>();
			mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SOMEUSER");
			mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
			controller.ControllerContext = mock.Object;

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateMethodsAlwaysReturnsCreateView()
		{
			//Arrange
			BooksController controller = new BooksController();
			var mock = new Mock<ControllerContext>();
			mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SomeUser");
			mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
			controller.ControllerContext = mock.Object;

			//Act
			ViewResult result = controller.Create() as ViewResult;

			//Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateBookCreatesNewBook()
		{
			FakeApplciationDbContext dbContext = new FakeApplciationDbContext {Books = new FakeDbSet<Book>()};

			BooksController controller = new BooksController(dbContext);
			var mock = new Mock<ControllerContext>();
			mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SOMEUSER");
			mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
			controller.ControllerContext = mock.Object;
			Book book = new Book()
			{
				Title = "Thing Explainer",
				Isbn = "9780544668256",
				TotalPageCount = 64,
				ThumbnailLink = "http://books.google.com/books/content?id=T5xXrgEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api"
			};

			//Act
			controller.Create(book);

			//Assert
			Assert.AreEqual("9780544668256", dbContext.Books.FirstOrDefault().Isbn);
		}
	}
}
