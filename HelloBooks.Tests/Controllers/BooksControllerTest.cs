using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloBooks;
using HelloBooks.Controllers;
using HelloBooks.Models;
using HelloBooks.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using Moq;

namespace HelloBooks.Tests.Controllers
{
	[TestClass]
	public class BooksControllerTest
	{
		private Mock<IPrincipal> mockPrincipal;
		private string userName = "test@test.com";
		private Mock<ControllerContext> mock;
		private IPrincipal principal;
		private FakeApplciationDbContext dbContext;
		private BooksController controller;


		[TestInitialize]
		public  void Init()
		{
			//Arrange
			var identity = new GenericIdentity(userName,"");
			var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, userName);
			identity.AddClaim(nameIdentifierClaim);
			
			mockPrincipal = new Mock<IPrincipal>();
			mockPrincipal.Setup(x => x.Identity).Returns(identity);
			mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
			//mockPrincipal.Setup(x => x.Identity.GetUserId()).Returns(userName);
			principal = mockPrincipal.Object;

			mock = new Mock<ControllerContext>();
			mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns(principal.Identity.GetUserName);
			mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
			
			dbContext	= new FakeApplciationDbContext { Books = new FakeDbSet<Book>() };
			controller = new BooksController(dbContext,principal);
			controller.ControllerContext = mock.Object;
			
		}

		[TestMethod]
		public void ShouldGetUserIdFromIdentity()
		{
			//Act
			var result = principal.Identity.GetUserId();

			//asserts
			Assert.AreEqual(userName, result);
		}


		[TestMethod]
		public void IdentityShouldBeAuthenticated()
		{
			//asserts
			Assert.IsTrue(principal.Identity.IsAuthenticated);
		}


		[TestMethod]
		public void BooksIndexAlwaysReturnsLibrary()
		{
			// Arrange

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateMethodsAlwaysReturnsCreateView()
		{
			//Arrange

			//Act
			ViewResult result = controller.Create() as ViewResult;

			//Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateBookCreatesNewBook()
		{
			Book book = new Book()
			{
				ApplicationUserId = principal.Identity.GetUserId(),
				Title = "Thing Explainer",
				Isbn = "9780544668256",
				TotalPageCount = 64,
				ThumbnailLink = "http://books.google.com/books/content?id=T5xXrgEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api"
			};

			//Act
			controller.Create(book);

			//Assert
			// ReSharper disable once PossibleNullReferenceException
			Assert.AreEqual("9780544668256", dbContext.Books.FirstOrDefault().Isbn);
		}

		[TestMethod]
		public void IsbnSearchReturnsCorrectItem()
		{
			//Arrange
			int expectedPageCount = 64;

			//Act
			ViewResult result = controller.ISBNSearch("9780544668256") as ViewResult;

			//Assert
			//Assert.AreEqual(expectedPageCount,result.Model.TotalPageCount);
			if (result != null)
			{
				Assert.IsInstanceOfType(result.Model, typeof(Book));
				Book resultLookup = result.Model as Book;

				// ReSharper disable once PossibleNullReferenceException
				Assert.AreEqual(expectedPageCount, resultLookup.TotalPageCount);
			}
		}

		[TestMethod]
		public void BookDetailsReturnsView()
		{
			//Arrange
			//var userStore = new Mock<IUserStore<ApplicationUser>>();
			//var userManager = new UserManager<ApplicationUser>(userStore.Object);
			dbContext.ApplicationUsers = new FakeDbSet<ApplicationUser>();

			
			ApplicationUser user = new ApplicationUser()
			{
				Id = principal.Identity.GetUserId(),
				Email = principal.Identity.GetUserName(),
				UserName = principal.Identity.GetUserName(),
				FirstName = "User",
				LastName = "Name"
			};
			dbContext.ApplicationUsers.Add(user);

			Book book = new Book()
			{
				Id = 1,
				User = user,
				ApplicationUserId = principal.Identity.GetUserId(),
				Title = "Thing Explainer",
				Isbn = "9780544668256",
				TotalPageCount = 64,
				ThumbnailLink = "http://books.google.com/books/content?id=T5xXrgEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api"
			};
			dbContext.Books.Add(book);
			

			//Act
			ViewResult result = controller.Details(1) as ViewResult;

			//Assert
			Assert.IsNotNull(result);
			//Assert.AreEqual(1,1);
		}
	}
}
