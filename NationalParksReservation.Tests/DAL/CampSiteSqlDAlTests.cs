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
    public class CampSiteSqlDAlTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;
        private string nameOfCampground = "";
        
        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT name, open_from_mm, open_to_mm, daily_fee FROM campground WHERE campground_id = 1;", connection);
                nameOfCampground = (string)cmd.ExecuteScalar();
            }
        }

        [TestCleanup()]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void RetrieveInfoTest()
        {
            //Arrange
            CampSiteSqlDAL campgroundSqlDal = new CampSiteSqlDAL(connectionString);

            //Act
            CampSearch campsearch = campgroundSqlDal.RetrieveInfo(1);

            //Assert
            Assert.IsNotNull(campsearch);
            Assert.AreEqual(nameOfCampground, campsearch.CampgroundName);
        }
    }
}
