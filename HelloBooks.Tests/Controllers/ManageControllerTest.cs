using System;
using System.Web.Mvc;
using HelloBooks.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloBooks.Tests.Controllers
{
	[TestClass]
	public class ManageControllerTest
	{
		[TestMethod]
		public void ManageAddPhoneNumberReturnsAddPhoneNumberView()
		{
			//Arrange
			ManageController controller = new ManageController();

			//Act
			ViewResult result = controller.AddPhoneNumber() as ViewResult;

			//Assert
			Assert.IsNotNull(result);
		}
	}
}
