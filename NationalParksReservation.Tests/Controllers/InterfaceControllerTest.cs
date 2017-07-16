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

namespace NationalParksReservation.Tests.Controllers
{
    [TestClass()]
    public class InterfaceControllerTest
    {
        [TestMethod()]
        public void InterfaceController_IndexAction_ReturnIndexView()
        {
            // Arrange
            InterfaceController controller = new InterfaceController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void InterfaceController_ParkInterfaceAction_ReturnParkInterfaceView()
        {
            // Arrange
            InterfaceController controller = new InterfaceController();

            // Act
            ViewResult result = controller.ParkInterface(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkInterface", result.ViewName);
        }
    }
}
