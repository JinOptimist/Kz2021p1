using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class DepartingFlightInfo : FlightBaseModel
    {
        public string FlightId { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public DateTime SetDate { set { DepartureTime = value.ToString("dd.MM.yyyy hh:mm"); } }
    }
}
