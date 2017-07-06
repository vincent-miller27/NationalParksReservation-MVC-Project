using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParksReservation.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public string ParkName { get; set; }
        public string CampgroundName { get; set; }
        public string OpenMonth { get; set; }
        public string CloseMonth { get; set; }
        public double DailyFee { get; set; }

        public string ToMonth(int month)
        {
            if (month == 1)
            {
                return "January";
            }
            else if (month == 2)
            {
                return "February";
            }
            else if (month == 3)
            {
                return "March";
            }
            else if (month == 4)
            {
                return "April";
            }
            else if (month == 5)
            {
                return "May";
            }
            else if (month == 6)
            {
                return "June";
            }
            else if (month == 7)
            {
                return "July";
            }
            else if (month == 8)
            {
                return "August";
            }
            else if (month == 9)
            {
                return "September";
            }
            else if (month == 10)
            {
                return "October";
            }
            else if (month == 11)
            {
                return "November";
            }
            else if (month == 12)
            {
                return "December";
            }
            return null;
        }
    }
}