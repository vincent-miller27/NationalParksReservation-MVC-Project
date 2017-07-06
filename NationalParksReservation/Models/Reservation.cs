using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NationalParksReservation.Models
{
    public class Reservation
    {
        [Required]
        public string ReservationName { get; set; }
        public int ReservationID { get; set; }
        public string CampgroundName { get; set; }
        public int SiteID { get; set; }
        public double DailyFee { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public DateTime ReservationBooked { get; set; }

        public Reservation()
        {
        }

        public Reservation(int siteId, DateTime arrivalDate, DateTime depatureDate)
        {
            SiteID = siteId;
            ReservationStart = arrivalDate;
            ReservationEnd = depatureDate;
        }

        public Reservation(int siteId, DateTime arrivalDate, DateTime depatureDate, double cost)
        {
            SiteID = siteId;
            ReservationStart = arrivalDate;
            ReservationEnd = depatureDate;
            DailyFee = cost;
        }
    }
}