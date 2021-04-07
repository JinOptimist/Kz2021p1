using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Airport
{
    public class IncomingFlightInfoViewModel
    {
        public string FlightId { get; set; }
        public string Origin { get; set; }
        public string ETA { get; set; }
        public string Status { get; set; }
        public string Airline { get; set; }
    }
}
