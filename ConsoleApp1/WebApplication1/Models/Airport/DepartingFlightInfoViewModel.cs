using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.Models.Airport
{
    public class DepartingFlightInfoViewModel
    {
        public string TailNumber { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public FlightStatus FlightStatus { get; set; }
        public string Airline { get; set; }
    }
}
