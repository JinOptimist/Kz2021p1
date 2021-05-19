using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces
{
    public interface IFlightsRepository : IBaseRepository<Flight>
    {
        /// <summary>
        /// Returns List of flights which have incoming flight type
        /// </summary>
        /// <returns></returns>
        List<Flight> GetAllIncomingFlights();
        /// <summary>
        /// Returns flights which have free seats and flight status set to On Time
        /// </summary>
        /// <returns></returns>
        List<Flight> GetFlightsAvailableForBooking();
        /// <summary>
        /// Returns flights which have flight status set to Landed
        /// </summary>
        /// <returns></returns>
        List<Flight> GetLandedFlights();
        /// <summary>
        /// Returns all flights within 5 minutes until arrival
        /// </summary>
        /// <returns></returns>
        List<Flight> GetArrivingFlights();
        /// <summary>
        /// Returns all flights within 5 minutes until departure, but not departed yet
        /// </summary>
        /// <returns></returns>
        List<Flight> GetDepartingFlights();
        /// <summary>
        /// Returns all flights which have FlightStatus set to Departed
        /// </summary>
        /// <returns></returns>
        List<Flight> GetDepartedFlights();
        /// <summary>
        /// Check if database has any flights
        /// </summary>
        /// <returns></returns>
        bool HasAnyFlights();
    }
}
