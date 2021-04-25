using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class Flight : BaseModel
    {
        public string TailNumber { get; set; }
        public FlightType FlightType { get; set; }
        public string Airline { get; set; }
        public FlightStatus FlightStatus { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Passenger> Passengers { get; set; }
    }
}
