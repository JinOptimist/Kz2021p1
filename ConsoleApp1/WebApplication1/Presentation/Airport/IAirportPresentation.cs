using System;
using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.Models.Airport;

namespace WebApplication1.Presentation.Airport
{
    public interface IAirportPresentation
    {
        void BookTicket(long id);
        bool FlightIsValid(long id);
        List<AvailableFlightsViewModel> GetAvailableFlights();
        List<IncomingFlightInfoViewModel> GetIndexViewModel();
        bool FlightIsAlreadyBooked(long id);
    }
}