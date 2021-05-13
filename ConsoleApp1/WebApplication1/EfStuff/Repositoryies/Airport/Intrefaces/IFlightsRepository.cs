using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces
{
    public interface IFlightsRepository : IBaseRepository<Flight>
    {
        List<Flight> GetAllIncomingFlights();
        List<Flight> GetFlightsAvailableForBooking();
    }
}
