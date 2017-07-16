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
    public class ReservationSqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;
        //Since the query always returns the top 5 results
        private int numberOfCampsites = 5;
        private int reservationNumber = 0;
        DateTime today = DateTime.Today;

        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand($"INSERT INTO reservation (site_id, name, to_date, from_date) VALUES (1, 'Test', '{today}', '{today}');  SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                reservationNumber = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup()]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void FindReservationTest()
        {
            //Arrange
            ReservationSqlDAL reservationSqlDal = new ReservationSqlDAL(connectionString);
            CampSearch cs = new CampSearch()
            {
                CampgroundId = 1,
                ArrivalDate = DateTime.Today,
                DepartureDate = DateTime.Today,
                MaxOccupancy = 4,
                IsAccessible = false,
                RVLength = 0,
                NeedUtilities = false
            };

            //Act
            List<CampSite> campSites = reservationSqlDal.FindReservation(cs);

            //Assert
            Assert.IsNotNull(campSites);
            Assert.AreEqual(numberOfCampsites, campSites.Count);
        }

        [TestMethod()]
        public void FindParkReservationTest()
        {
            //Arrange
            ReservationSqlDAL reservationSqlDal = new ReservationSqlDAL(connectionString);
            ParkSearch ps = new ParkSearch()
            {
                ParkId = 1,
                ArrivalDate = DateTime.Today,
                DepartureDate = DateTime.Today,
                MaxOccupancy = 4,
                IsAccessible = false,
                RVLength = 0,
                NeedUtilities = false
            };

            //Act
            List<CampSite> campSites = reservationSqlDal.FindParkReservation(ps);

            //Assert
            Assert.IsNotNull(campSites);
            Assert.AreEqual(numberOfCampsites, campSites.Count);
        }

        [TestMethod()]
        public void CreateReservationTest()
        {
            //Arrange
            ReservationSqlDAL reservationSqlDal = new ReservationSqlDAL(connectionString);

            //Act
            int reservation = reservationSqlDal.CreateReservation(3, today, today, "Test");

            //Assert
            Assert.IsNotNull(reservation);
            Assert.AreEqual(reservation, reservationNumber + 1);
        }
    }
}
