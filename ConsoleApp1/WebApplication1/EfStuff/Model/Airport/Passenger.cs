using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class Passenger
    {
        public long Id { get; set; }
        public long FlightId { get; set; }
        public long CitizenId { get; set; }
    }
}
