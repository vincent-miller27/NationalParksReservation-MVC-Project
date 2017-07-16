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
            //Session will always be filled but If/Else statement is there for testing purposes
            ParkSearch existingParkSearch = new ParkSearch();
            if (Session["ParkSearch"] != null)
            {
                existingParkSearch = Session["ParkSearch"] as ParkSearch;
            }
            else
            {
                existingParkSearch = new ParkSearch()
                {
                    ArrivalDate = DateTime.Today,
                    DepartureDate = DateTime.Today
                };
            }

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
            //Session will always be filled but If/Else statement is there for testing purposes
            CampSearch existingSearch = new CampSearch();
            if (Session["ParkSearch"] != null)
            {
                existingSearch = Session["CampSearch"] as CampSearch;
            }
            else
            {
                existingSearch = new CampSearch()
                {
                    ArrivalDate = DateTime.Today,
                    DepartureDate = DateTime.Today,
                    DailyFee = 0.00
                };
            }

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