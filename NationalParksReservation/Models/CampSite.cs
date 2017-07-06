using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParksReservation.Models
{
    public class CampSite
    {
        public string Campground { get; set; }
        public int SiteId { get; set; }
        public int SiteNumber { get; set; }
        public int CampgroundId { get; set; }
        public int MaxOccupancy { get; set; }
        public string Accessible { get; set; }
        public int MaxRvLength { get; set; }
        public string Utilities { get; set; }
        public double Cost { get; set; }

        public string BoolToString(bool isTrue)
        {
            if (isTrue)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
    }
}