using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace NationalParksReservation.Models
{
    public class CampSearch
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        public int CampgroundId { get; set; }
        public int SiteId { get; set; }
        public string CampgroundName { get; set; }
        public string OpenMonth { get; set; }
        public int OpenMonthNumber { get; set; }
        public string CloseMonth { get; set; }
        public int CloseMonthNumber { get; set; }
        public bool IsAccessible { get; set; }
        public int RVLength { get; set; }
        public bool NeedUtilities { get; set; }
        public double DailyFee { get; set; }
    }
}