using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.Models.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
	public class FlightsRepository : BaseRepository<Flight>, IFlightsRepository
    {
        public FlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public List<Flight> GetAllIncomingFlights()
        {
            return _dbSet
                .Where(flight => flight.FlightType == FlightType.IncomingFlight)
                .ToList();
        }
        /// <summary>
        /// Returns all flights within 10 minutes until arrival
        /// </summary>
        /// <returns>List of flights</returns>
        public List<Flight> GetArrivingFlights()
        {
            var currentTime = DateTime.Now;
            var offset = currentTime.AddMinutes(10);
            return _dbSet
                .Where(f => f.FlightType == FlightType.IncomingFlight
                && currentTime >= f.Date && currentTime <= offset).ToList();
        }
        /// <summary>
        /// Returns all flights within 10 minutes until departure
        /// </summary>
        /// <returns>List of flights</returns>
        public List<Flight> GetDepartingFlights()
        {
            var currentTime = DateTime.Now;
            var offset = currentTime.AddMinutes(-10);
            return _dbSet
                .Where(f => f.FlightType == FlightType.DepartingFlight
                && currentTime >= offset && currentTime <= f.Date).ToList();
        }
        public List<Flight> GetFlightsAvailableForBooking()
        {
            return _dbSet
                .Where(flight => flight.FlightStatus == FlightStatus.OnTime && flight.FlightType == FlightType.DepartingFlight && flight.Passengers.Count() < 100)
                .ToList();
        }
    }
}
