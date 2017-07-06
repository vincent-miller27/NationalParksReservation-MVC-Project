using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalParksReservation;
using NationalParksReservation.Controllers;
using NationalParksReservation.Models;
using Moq;

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass]
    public class CampgroundControllerTest
    {
        [TestMethod]
        public void CampgroundIndex()
        {
            // Arrange
            CampgroundController controller = new CampgroundController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ParkSearch_HttpGet()
        {
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
            // Arrange
            ParkSearchSqlDAL mockDal = new ParkSearchSqlDAL(connectionString);
            CampgroundController controller = new CampgroundController();
            ParkSearch model = new ParkSearch();

            // Act
            ViewResult result = controller.ParkSearch(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkSearch", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.ReferenceEquals(model, mockDal.RetrieveInfo(1));
        }

        [TestMethod]
        public void ParkSearch_HttpPost()
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

        [TestMethod]
        public void Search_HttpGet()
        {
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
            // Arrange
            CampSiteSqlDAL mockDal = new CampSiteSqlDAL(connectionString);
            CampgroundController controller = new CampgroundController();
            CampSearch model = new CampSearch();

            // Act
            ViewResult result = controller.Search(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Search", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.ReferenceEquals(model, mockDal.RetrieveInfo(1));
        }

        [TestMethod]
        public void Search_HttpPost()
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
