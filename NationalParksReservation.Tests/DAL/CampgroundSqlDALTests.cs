using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalParksReservation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Web.Mvc;
using NationalParksReservation.DAL;
using NationalParksReservation.Models;

namespace NationalParksReservation.Tests.DAL
{
    [TestClass()]
    public class CampgroundSqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;
        private int numberOfCampgrounds = 0;

        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(c.campground_id) FROM campground c JOIN park p ON p.park_id = c.park_id WHERE c.park_id = 1;", connection);
                numberOfCampgrounds = (int)cmd.ExecuteScalar();
            }

        }

        [TestCleanup()]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetParksTest()
        {
            //Arrange
            CampgroundSqlDAL campgroundSqlDal = new CampgroundSqlDAL(connectionString);

            //Act
            List<Campground> campground = campgroundSqlDal.ListCampgrounds(1);

            //Assert
            Assert.IsNotNull(campground);
            Assert.AreEqual(numberOfCampgrounds, campground.Count);
        }
    }
}
