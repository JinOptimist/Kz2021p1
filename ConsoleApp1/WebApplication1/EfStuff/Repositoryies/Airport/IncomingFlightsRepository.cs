using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class IncomingFlightsRepository : AirportBaseRepository<IncomingFlightInfo>
    {
        public IncomingFlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public void PopulateIncomingFlights()
        {
            Random random = new Random();
            var rowsInc = from o in _kzDbContext.IncomingFlightsInfo select o;
            foreach (var row in rowsInc)
            {
                Remove(row);
            }
            string[] FlightsIdPool = new[] { "DV 701", "KC 671", "DV 703", "IQ 354", "IQ 408", "KC 7051" };
            string[] PlasesPool = new[] { "Dublin", "Moscow", "New-York", "London", "Tokyo", "Paris" };
            string[] IncomingStatusesPool = new[] { "Landed", "Expected", "Delayed" };
            string[] DepartingStatusesPool = new[] { "Canceled", "On Time", "Delayed", "Departed" };
            Console.WriteLine("Creating incoming flights...");
            for (int i = 0; i < random.Next(5, 10); i++)
            {
                IncomingFlightInfo incomingFlight = new IncomingFlightInfo
                {
                    FlightId = FlightsIdPool[random.Next(FlightsIdPool.Length)],
                    Origin = PlasesPool[random.Next(PlasesPool.Length)],
                    SetDate = DateTime.Today.AddHours(random.Next(5)),
                    Status = IncomingStatusesPool[random.Next(IncomingStatusesPool.Length)],
                    Airline = "International Airlines",
                };
                Save(incomingFlight);
            }
            Console.WriteLine("Incoming flights are created");
        }
    }
}
