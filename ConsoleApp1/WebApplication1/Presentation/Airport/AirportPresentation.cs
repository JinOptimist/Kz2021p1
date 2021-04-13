using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Models.Airport;

namespace WebApplication1.Presentation.Airport
{
    public class AirportPresentation
    {
        private FlightsRepository _flightsRepository { get; set; }

        public AirportPresentation(FlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }


        public List<IncomingFlightInfoViewModel> GetIndexViewModel()
        {
            // TODO: Inject automapper
            return _flightsRepository.GetAll().Where(flight => flight.FlightType == FlightType.IncomingFlight).Select(x => new IncomingFlightInfoViewModel()
            {
                TailNumber = x.TailNumber,
                Airline = x.Airline,
                Origin = x.Place,
                ETA = x.Date.ToString("dd.MM.yyyy HH:mm"),
                FlightStatus = x.FlightStatus
            }).ToList();
        }

        public List<Flight> GetAvailableFlights()
        {
            return _flightsRepository.GetAll().Where(flight => flight.FlightStatus == FlightStatus.OnTime && flight.FlightType == FlightType.DepartingFlight).ToList();
        }
    }
}
