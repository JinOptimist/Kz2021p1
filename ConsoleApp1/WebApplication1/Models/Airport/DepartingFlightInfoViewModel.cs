using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Airport
{
    public class DepartingFlightInfoViewModel
    {
        public string FlightId { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string Status { get; set; }
        public string Airline { get; set; }
    }
}
