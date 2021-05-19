using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class PassengersRepository : BaseRepository<Passenger>, IPassengersRepository
    {
        public PassengersRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public Passenger GetPassengerByCitizenId(long citizenId)
        {
            return _dbSet.SingleOrDefault(p => p.CitizenId == citizenId);
        }

        //public List<Passenger> GetAllPassengersAvailableForAdmission()
        //{
        //    var incoming = _dbSet.Where(p => p.Flights.Where(f => f.FlightType == FlightType.IncomingFlight))
        //    //var incomingPassengers = _dbSet
        //    //    .Where(passenger => passenger.Flights.Where(f => f.FlightType == FlightType.IncomingFlight).ToList())
        //    //    .ToList();
        //    //return incomingPassengers
        //    //    .Where(passenger => IsValidAddmissionTime(passenger.Flight.Date))
        //    //    .ToList();
        //}

        //public List<Passenger> GetAllPassengersAvailableForDeparture()
        //{
        //    var departingPassengers = _dbSet
        //        .Where(passenger => passenger.Flight.FlightType == FlightType.IncomingFlight)
        //        .ToList();
        //    return departingPassengers
        //        .Where(passenger => IsValidDepartureTime(passenger.Flight.Date))
        //        .ToList();
        //}

        //public bool CitizenIsRegisteredForFlight(long flightId, long citizenId)
        //{
        //    return _dbSet.SingleOrDefault(passenger => passenger.Citizen.Id == citizenId && passenger.Flight.Id == flightId) != null;
        //}

        //private bool IsValidDepartureTime(DateTime flightDate)
        //{
        //    DateTime now = DateTime.Now;
        //    if (flightDate.Day == now.Day && flightDate.AddMinutes(-30) <= now && now >= flightDate)
        //        return true;
        //    return false;
        //}

        //private bool IsValidAddmissionTime(DateTime flightDate)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime add30 = flightDate.AddMinutes(30);
        //    if (flightDate.Day == now.Day && now >= flightDate && now <= add30)
        //        return true;
        //    return false;
        //}

        //public List<Passenger> GetAllPassengersAvailableForAdmission()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Passenger> GetAllPassengersAvailableForDeparture()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool CitizenIsRegisteredForFlight(long flightId, long citizenId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
