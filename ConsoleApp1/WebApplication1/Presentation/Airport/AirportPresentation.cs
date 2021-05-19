using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models.Airport;
using WebApplication1.Services;

namespace WebApplication1.Presentation.Airport
{
    public class AirportPresentation : IAirportPresentation
    {
        private IFlightsRepository _flightsRepository { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private IPassengersRepository _passengersRepository { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        public AirportPresentation(IFlightsRepository flightsRepository, IMapper mapper,
             IUserService userService, ICitizenRepository citizenRepository, IPassengersRepository passengersRepository)
        {
            _flightsRepository = flightsRepository;
            _mapper = mapper;
            _userService = userService;
            _citizenRepository = citizenRepository;
            _passengersRepository = passengersRepository;
        }


        public List<IncomingFlightInfoViewModel> GetIndexViewModel()
        {
            return _flightsRepository
                .GetAllIncomingFlights()
                .Select(flights => _mapper.Map<IncomingFlightInfoViewModel>(flights))
                .ToList();
        }

        public List<AvailableFlightsViewModel> GetAvailableFlights()
        {
            return _flightsRepository
                .GetFlightsAvailableForBooking()
                .Select(flight => _mapper.Map<AvailableFlightsViewModel>(flight))
                .ToList();
        }

        public bool FlightIsValid(long id)
        {
            return _flightsRepository.Get(id) != null;
        }

        public bool FlightIsAlreadyBooked(long id)
        {
            var isBooked = _userService.GetUser().Passenger?.Flights.Any(f => f.Id == id);
            return isBooked ?? false;
        }

        public void BookTicket(long id)
        {
            var selectedFlight = _flightsRepository.Get(id);
            var citizen = _userService.GetUser();
            var passenger = _passengersRepository.GetPassengerByCitizenId(citizen.Id) ?? _mapper.Map<Passenger>(citizen);

            passenger.Citizen ??= citizen;
            passenger.Flights ??= new List<Flight>();
            citizen.Passenger ??= passenger;

            passenger.Flights.Add(selectedFlight);
            selectedFlight.Passengers.Add(passenger);

            _passengersRepository.Save(passenger);
            _citizenRepository.Save(citizen);
            _flightsRepository.Save(selectedFlight);
        }
    }
}
