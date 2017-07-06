using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationalParksReservation.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public string ParkLocation { get; set; }
        public DateTime DateEstablished { get; set; }
        public int ParkAreaSqAcres { get; set; }
        public int AnnualParkVisitorCount { get; set; }
        public string ParkDescription { get; set; }
    }
}