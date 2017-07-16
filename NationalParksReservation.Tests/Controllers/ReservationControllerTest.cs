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
using System.Transactions;
using System.Data.SqlClient;

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass()]
    public class ReservationControllerTest
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;

        //To allow for successful testing where Actions call on a DAL method without adding to database
        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }

        }

        [TestCleanup()]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void ReservationController_IndexAction_ReturnIndexView()
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

        [TestMethod()]
        public void ReservationController_ParkScreenAction_ReturnParkScreenView()
        {
            // Arrange
            ReservationController controller = new ReservationController();
            DateTime arrive = Convert.ToDateTime("07/04/2017");
            DateTime depart = Convert.ToDateTime("07/05/2017");
            ParkSearch parkSearch = new ParkSearch()
            {
                ArrivalDate = arrive,
                DepartureDate = depart,
                MaxOccupancy = 0,
                ParkId = 1,
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
            ViewResult result = controller.ParkScreen(parkSearch) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkScreen", result.ViewName);
        }

        [TestMethod()]
        public void ReservationController_ParkSubmitAction_ReturnParkSubmitView()
        {
            //Arrange
            ReservationController controller = new ReservationController();

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
            ViewResult result = controller.ParkSubmit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkSubmit", result.ViewName);
        }

        [TestMethod()]
        public void ReservationController_ParkSearchAction_RedirectToConfirmationView()
        {
            //Arrange
            ReservationController controller = new ReservationController();
            Reservation reservation = new Reservation();

            //Act
            RedirectToRouteResult result = controller.ParkSubmit(reservation) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Confirmation", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ReservationController_SubmitAction_ReturnSubmitView()
        {
            //Arrange
            ReservationController controller = new ReservationController();

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
            ViewResult result = controller.Submit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Submit", result.ViewName);
        }

        [TestMethod()]
        public void ReservationController_SearchAction_RedirectToConfirmationView()
        {
            //Arrange
            ReservationController controller = new ReservationController();
            Reservation reservation = new Reservation();

            //Act
            RedirectToRouteResult result = controller.Submit(reservation) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Confirmation", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ReservationController_ConfirmationAction_ReturnConfirmationView()
        {
            //Arrange
            ReservationController controller = new ReservationController();
            Reservation reservation = new Reservation()
            {
                SiteID = 1,
                ReservationStart = DateTime.Today,
                ReservationEnd = DateTime.Today,
                ReservationName = "Test"
            };
            
            //Act
            ViewResult result = controller.Confirmation(reservation) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Confirmation", result.ViewName);
        }
    }
}

