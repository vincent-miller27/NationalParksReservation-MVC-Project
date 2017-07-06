using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationalParksReservation.Models;
using NationalParksReservation.DAL;

namespace NationalParksReservation.Controllers
{
    public class InterfaceController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";
        
        // GET: Index
        public ActionResult Index()
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            List<Park> parkList = parkDAL.GetAllParks();
            return View("Index", parkList);
        }
        //Get: ParkInterface
        public ActionResult ParkInterface(int id)
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            Park park = parkDAL.GenerateParkName(id);
            return View("ParkInterface", park);
        }
    }
}