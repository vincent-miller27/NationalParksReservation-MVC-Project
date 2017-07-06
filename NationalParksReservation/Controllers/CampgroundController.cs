using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationalParksReservation.Models;
using NationalParksReservation.DAL;

namespace NationalParksReservation.Controllers
{
    public class CampgroundController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";

        // GET: Campground
        public ActionResult Index(int id)
        {
            CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgroundList = campgroundDAL.ListCampgrounds(id);

            return View("Index", campgroundList);
        }

        //GET: ParkSearch
        public ActionResult ParkSearch(int id)
        {
            ParkSearchSqlDAL parkSearchDAL = new ParkSearchSqlDAL(connectionString);
            ParkSearch parkSearch = parkSearchDAL.RetrieveInfo(id);

            return View("ParkSearch", parkSearch);
        }

        [HttpPost]
        public ActionResult ParkSearch(ParkSearch parkSearch)
        {
            return RedirectToAction("ParkScreen", "Reservation", parkSearch);
        }

        //GET: Search
        public ActionResult Search(int id)
        {
            CampSiteSqlDAL campSiteDAL = new CampSiteSqlDAL(connectionString);
            CampSearch campSearch = campSiteDAL.RetrieveInfo(id);

            return View("Search", campSearch);
        }

        [HttpPost]
        public ActionResult Search(CampSearch campSearch)
        {
            return RedirectToAction("Index", "Reservation", campSearch);
        }
    }
}