using AutoMapper;
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
        private IMapper _mapper { get; set; }
        public AirportPresentation(FlightsRepository flightsRepository, IMapper mapper)
        {
            _flightsRepository = flightsRepository;
            _mapper = mapper;
        }


        public List<IncomingFlightInfoViewModel> GetIndexViewModel()
        {
            return _flightsRepository
                .GetAll()
                .Where(flight => flight.FlightType == FlightType.IncomingFlight)
                .Select(flights => _mapper.Map<IncomingFlightInfoViewModel>(flights))
                .ToList();
        }

        public List<Flight> GetAvailableFlights()
        {
            return _flightsRepository
                .GetAll()
                .Where(flight => flight.FlightStatus == FlightStatus.OnTime && flight.FlightType == FlightType.DepartingFlight)
                .ToList();
        }
    }
}
