using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NationalParksReservation.Models
{
    public class ParkSearch
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        public int ParkId { get; set; }
        public int SiteId { get; set; }
        public string ParkName { get; set; }
        public bool IsAccessible { get; set; }
        public int RVLength { get; set; }
        public bool NeedUtilities { get; set; }
    }
}