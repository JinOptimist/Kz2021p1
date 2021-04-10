using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class DepartingFlightsRepository : AirportBaseRepository<DepartingFlightInfo>
    {
        public DepartingFlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public void PopulateDepartingFlights()
        {
            Random random = new Random();
            var rowsInc = from o in _kzDbContext.DepartingFlightsInfo select o;
            foreach (var row in rowsInc)
            {
                Remove(row);
            }
            string[] FlightsIdPool = new[] { "DV 701", "KC 671", "DV 703", "IQ 354", "IQ 408", "KC 7051" };
            string[] PlasesPool = new[] { "Dublin", "Moscow", "New-York", "London", "Tokyo", "Paris" };
            string[] IncomingStatusesPool = new[] { "Landed", "Expected", "Delayed" };
            string[] DepartingStatusesPool = new[] { "Canceled", "On Time", "Delayed", "Departed" };
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                DepartingFlightInfo departingFlight = new DepartingFlightInfo
                {
                    FlightId = FlightsIdPool[random.Next(FlightsIdPool.Length)],
                    Destination = PlasesPool[random.Next(PlasesPool.Length)],
                    SetDate = DateTime.Today.AddHours(random.Next(5)),
                    Status = DepartingStatusesPool[random.Next(DepartingStatusesPool.Length)],
                    Airline = "International Airlines",
                };
                Save(departingFlight);
            }
        }
    }
}
