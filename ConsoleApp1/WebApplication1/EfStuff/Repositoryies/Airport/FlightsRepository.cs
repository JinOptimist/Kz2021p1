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
        /// <summary>
        /// Returns List of flights which have incoming flight type
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetAllIncomingFlights()
        {
            return _dbSet
                .Where(flight => flight.FlightType == FlightType.IncomingFlight)
                .ToList();
        }
        /// <summary>
        /// Returns all flights within 5 minutes until arrival
        /// </summary>
        /// <returns>List of flights</returns>
        public List<Flight> GetArrivingFlights()
        {
            var currentTime = DateTime.Now;
            var offset = currentTime.AddMinutes(-5);
            return _dbSet
                .Where(f => f.FlightType == FlightType.IncomingFlight
                && offset <= f.Date && f.Date <= currentTime).ToList();
        }
        /// <summary>
        /// Returns all flights within 5 minutes until departure
        /// </summary>
        /// <returns>List of flights</returns>
        public List<Flight> GetDepartingFlights()
        {
            var currentTime = DateTime.Now;
            var offset = currentTime.AddMinutes(-5);
            return _dbSet
                .Where(f => f.FlightType == FlightType.DepartingFlight
                && f.FlightStatus != FlightStatus.Canceled && f.FlightStatus != FlightStatus.Departed
                && offset <= f.Date && f.Date <= currentTime).ToList();
        }
        /// <summary>
        /// Returns flights which have flight status set to Landed
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetLandedFlights()
        {
            return _dbSet.Where(f => f.FlightStatus == FlightStatus.Landed).ToList();
        }
        /// <summary>
        /// Returns all flights which have FlightStatus set to Departed
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetDepartedFlights()
        {
            return _dbSet
                .Where(f => f.FlightStatus == FlightStatus.Departed).ToList();
        }
        /// <summary>
        /// Returns flights which have free seats and flight status set to On Time
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetFlightsAvailableForBooking()
        {
            return _dbSet
                .Where(flight => flight.FlightStatus == FlightStatus.OnTime && flight.FlightType == FlightType.DepartingFlight && flight.Passengers.Count() < 100)
                .ToList();
        }

        public bool HasAnyFlights()
        {
            return _dbSet.Any();
        }
    }
}
