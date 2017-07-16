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
    public class ParkSearchSqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        private TransactionScope tran;
        private string nameOfPark = "";

        //To allow for successful testing of SurveySubmit HttpPost
        [TestInitialize()]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT name FROM park WHERE park_id = 1;", connection);
                nameOfPark = (string)cmd.ExecuteScalar();
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
            ParkSearchSqlDAL parkSeachSqlDal = new ParkSearchSqlDAL(connectionString);

            //Act
            ParkSearch parksearch = parkSeachSqlDal.RetrieveInfo(1);

            //Assert
            Assert.IsNotNull(parksearch);
            Assert.AreEqual(nameOfPark, parksearch.ParkName);
        }
    }
}
