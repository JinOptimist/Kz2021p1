using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class IncomingFlightInfo : FlightBaseModel
    {
        public string FlightId { get; set; }
        public string Origin { get; set; }
        public string ETA { get; set; }
        public DateTime SetDate { set { ETA = value.ToString("dd.MM.yyyy hh:mm"); } }
    }
}
