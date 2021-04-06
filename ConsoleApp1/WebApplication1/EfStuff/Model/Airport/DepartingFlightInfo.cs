using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class DepartingFlightInfo : FlightBaseModel
    {
        public string FlightId { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public DateTime SetDate { set { DepartureTime = value.ToString("dd.MM.yyyy hh:mm"); } }

        public bool DepartureIsNow()
        {
            DateTime now = DateTime.Now;
            DateTime dt = DateTime.Parse(DepartureTime);
            if (dt.Day == now.Day && dt.AddHours(-1).Hour <= now.Hour && now.Hour <= dt.Hour) 
                return true;
            return false;
        }
    }
}
