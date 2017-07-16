using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalParksReservation;
using NationalParksReservation.Controllers;
using NationalParksReservation.Models;
using NationalParksReservation.DAL;
using Moq;

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass()]
    public class CampgroundControllerTest
    {
        [TestMethod]
        public void CampgroundController_IndexAction_ReturnIndexView()
        {
            // Arrange
            CampgroundController controller = new CampgroundController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void CampgroundController_ParkSearchAction_ReturnParkSearchView()
        {
            // Arrange
            CampgroundController controller = new CampgroundController();

            // Act
            ViewResult result = controller.ParkSearch(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkSearch", result.ViewName);
        }

        [TestMethod()]
        public void CampgroundController_ParkSearchAction_RedirectToParkScreenView()
        {
            //Arrange
            CampgroundController controller = new CampgroundController();
            ParkSearch model = new ParkSearch();


            //Act
            RedirectToRouteResult result = controller.ParkSearch(model) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkScreen", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void CampgroundController_SearchAction_ReturnSearchView()
        {
            // Arrange
            CampgroundController controller = new CampgroundController();

            // Act
            ViewResult result = controller.Search(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Search", result.ViewName);
        }

        [TestMethod()]
        public void CampgroundController_ParkSearchAction_RedirectToIndexView()
        {
            //Arrange
            CampgroundController controller = new CampgroundController();
            CampSearch model = new CampSearch();


            //Act
            RedirectToRouteResult result = controller.Search(model) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
