using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.Models.Airport
{
    public class IncomingFlightInfoViewModel
    {
        public string TailNumber { get; set; }
        public string Origin { get; set; }
        public string ETA { get; set; }
        public FlightStatus FlightStatus { get; set; }
        public string Airline { get; set; }
    }
}
