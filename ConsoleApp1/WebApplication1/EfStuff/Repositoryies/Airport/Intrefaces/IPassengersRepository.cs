using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces
{
    public interface IPassengersRepository : IBaseRepository<Passenger>
    {
        Passenger GetPassengerByCitizenId(long citizenId);
        //List<Passenger> GetAllPassengersAvailableForAdmission();
        //List<Passenger> GetAllPassengersAvailableForDeparture();
        //bool CitizenIsRegisteredForFlight(long flightId, long citizenId);
    }
}
