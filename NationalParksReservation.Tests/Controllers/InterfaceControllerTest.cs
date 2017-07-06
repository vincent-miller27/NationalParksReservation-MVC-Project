using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalParksReservation;
using NationalParksReservation.Controllers;
using NationalParksReservation.Models;

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass]
    public class InterfaceControllerTest
    {
        [TestMethod]
        public void InterfaceIndex()
        {
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
            // Arrange
            ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
            InterfaceController controller = new InterfaceController();
            List<Park> model = new List<Park>();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.ReferenceEquals(model, mockDal.GetAllParks());
        }

        [TestMethod]
        public void ParkInterface()
        {
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
            // Arrange
            ParkSqlDAL mockDal = new ParkSqlDAL(connectionString);
            InterfaceController controller = new InterfaceController();
            Park model = new Park();

            // Act
            ViewResult result = controller.ParkInterface(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkInterface", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.ReferenceEquals(model, mockDal.GenerateParkName(1));
        }
    }
}
