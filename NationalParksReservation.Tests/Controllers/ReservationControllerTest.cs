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
using System.Web.Routing;
using System.Web;

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass]
    public class ReservationControllerTest
    {
        [TestMethod]
        public void ReservationIndex()
        {
            // Arrange
            ReservationController controller = new ReservationController();
            DateTime arrive = Convert.ToDateTime("07/04/2017");
            DateTime depart = Convert.ToDateTime("07/05/2017");
            CampSearch campSearch = new CampSearch()
            {
                CampgroundId = 1,
                ArrivalDate = arrive,
                DepartureDate = depart,
                MaxOccupancy = 0,
                IsAccessible = false,
                RVLength = 0,
                NeedUtilities = false
            };


            // Mock Session Object
            Mock<HttpSessionStateBase> mockSession = new Mock<HttpSessionStateBase>();

            // Mock Http Context Request for Controller
            // because Session doesn't exist unless MVC actually receives a "request"
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();

            // When the Controller calls this.Session it will get a mock session
            mockContext.Setup(c => c.Session).Returns(mockSession.Object);

            // Assign the context property on the controller to our mock context which uses our mock session
            controller.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), controller);


            // Act
            ViewResult result = controller.Index(campSearch) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void ParkScreen()
        {
            // Arrange
            ReservationController controller = new ReservationController();
            ParkSearch parkSearch = new ParkSearch();

            // Act
            ViewResult result = controller.ParkScreen(parkSearch) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkScreen", result.ViewName);
        }

        //[TestMethod]
        //public void InterfaceIndex()
        //{
        //    string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        //    // Arrange
        //    ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
        //    InterfaceController controller = new InterfaceController();
        //    List<Park> model = new List<Park>();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.ViewName);
        //    Assert.ReferenceEquals(model, mockDal.GetAllParks());
        //}

        //[TestMethod]
        //public void InterfaceIndex()
        //{
        //    string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        //    // Arrange
        //    ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
        //    InterfaceController controller = new InterfaceController();
        //    List<Park> model = new List<Park>();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.ViewName);
        //    Assert.ReferenceEquals(model, mockDal.GetAllParks());
        //}

        //[TestMethod]
        //public void InterfaceIndex()
        //{
        //    string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        //    // Arrange
        //    ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
        //    InterfaceController controller = new InterfaceController();
        //    List<Park> model = new List<Park>();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.ViewName);
        //    Assert.ReferenceEquals(model, mockDal.GetAllParks());
        //}

        //[TestMethod]
        //public void InterfaceIndex()
        //{
        //    string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        //    // Arrange
        //    ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
        //    InterfaceController controller = new InterfaceController();
        //    List<Park> model = new List<Park>();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.ViewName);
        //    Assert.ReferenceEquals(model, mockDal.GetAllParks());
        //}

        //[TestMethod]
        //public void InterfaceIndex()
        //{
        //    string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        //    // Arrange
        //    ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
        //    InterfaceController controller = new InterfaceController();
        //    List<Park> model = new List<Park>();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual("Index", result.ViewName);
        //    Assert.ReferenceEquals(model, mockDal.GetAllParks());
        //}
    }
}
