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
    public class ParkSqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;
        private int numberOfParks = 0;
        private int parkID = 0;
        
        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM park;", connection);
                numberOfParks = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT * FROM park WHERE park_id = 1;", connection);
                parkID = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup()]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetAllParksTest()
        {
            //Arrange
            ParkSqlDAL parkSqlDal = new ParkSqlDAL(connectionString);

            //Act
            List<Park> parks = parkSqlDal.GetAllParks();

            //Assert
            Assert.IsNotNull(parks);
            Assert.AreEqual(numberOfParks, parks.Count);
        }

        [TestMethod()]
        public void GenerateParkNameTest()
        {
            //Arrange
            ParkSqlDAL parkSqlDal = new ParkSqlDAL(connectionString);

            //Act
            Park park = parkSqlDal.GenerateParkName(1);

            //Assert
            Assert.IsNotNull(park);
            Assert.AreEqual(parkID, park.ParkId);
        }
    }
}
