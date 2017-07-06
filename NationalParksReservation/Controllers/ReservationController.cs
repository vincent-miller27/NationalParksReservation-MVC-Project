using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationalParksReservation.Models;
using NationalParksReservation.DAL;

namespace NationalParksReservation.Controllers
{
    public class ReservationController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalPark;Integrated Security=True";

        // GET: Index
        public ActionResult Index(CampSearch campSearch)
        {
            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString);

            Session["CampSearch"] = campSearch;

            List<CampSite> siteList = reservationDAL.FindReservation(campSearch);

            return View("Index", siteList);
        }

        public ActionResult ParkScreen(ParkSearch parkSearch)
        {
            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString);

            Session["ParkSearch"] = parkSearch;


            List<CampSite> siteList = reservationDAL.FindParkReservation(parkSearch);

            return View("ParkScreen", siteList);
        }

        public ActionResult ParkSubmit(int id)
        {
            ParkSearch existingParkSearch = Session["ParkSearch"] as ParkSearch;

            Reservation reservation = new Reservation(id, existingParkSearch.ArrivalDate, existingParkSearch.DepartureDate);

            return View("ParkSubmit", reservation);
        }

        [HttpPost]
        public ActionResult ParkSubmit(Reservation reservation)
        {
            return RedirectToAction("Confirmation", reservation);
        }

        // GET: Submit
        public ActionResult Submit(int id)
        {
            CampSearch existingSearch = Session["CampSearch"] as CampSearch;

            Reservation reservation = new Reservation(id, existingSearch.ArrivalDate, existingSearch.DepartureDate, existingSearch.DailyFee);

            return View("Submit", reservation);
        }

        [HttpPost]
        public ActionResult Submit(Reservation reservation)
        {
            return RedirectToAction("Confirmation", reservation);
        }

        //GET: Confirmation
        public ActionResult Confirmation(Reservation reservation)
        {
            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString);

            int reservationId = reservationDAL.CreateReservation(reservation.SiteID, reservation.ReservationStart, reservation.ReservationEnd, reservation.ReservationName);

            return View("Confirmation", reservationId);
        }
    }
} 