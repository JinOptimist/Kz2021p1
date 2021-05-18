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
        //private IPassengersRepository _passengersRepository { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        public AirportPresentation(IFlightsRepository flightsRepository, IMapper mapper,
             IUserService userService, ICitizenRepository citizenRepository)
        {
            _flightsRepository = flightsRepository;
            _mapper = mapper;
            _userService = userService;
            _citizenRepository = citizenRepository;
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

        public void AdmitPassengers()
        {
            //List<Flight> arrivedFlights = new List<Flight>();
            //foreach (var passenger in _passengersRepository.GetAllPassengersAvailableForAdmission())
            //{
            //    if (!arrivedFlights.Contains(passenger.Flight))
            //    {
            //        arrivedFlights.Add(passenger.Flight);
            //    }
            //    passenger.Citizen.IsOutOfCity = false;
            //    _passengersRepository.Save(passenger);
            //    _passengersRepository.Remove(passenger);
            //}
            //ConvertFlights(arrivedFlights);
            Debug.WriteLine("Admited");
        }

        public void DepartPassengers()
        {
            //List<Flight> departedFlights = new List<Flight>();
            //foreach (var passenger in _passengersRepository.GetAllPassengersAvailableForDeparture())
            //{
            //    if (!departedFlights.Contains(passenger.Flight))
            //    {
            //        departedFlights.Add(passenger.Flight);
            //    }
            //    passenger.Citizen.IsOutOfCity = true;
            //    _citizenRepository.Save(passenger.Citizen);
            //}
            //ConvertFlights(departedFlights);
            Debug.WriteLine("Departed");
        }

        public bool FlightIsValid(long id)
        {
            return _flightsRepository.Get(id) != null;
        }

        public bool FlightIsAlreadyBooked(long id)
        {
            //var citizen = _userService.GetUser();
            //return _passengersRepository.CitizenIsRegisteredForFlight(id, citizen.Id);
            return false;
        }

        public void BookTicket(long id)
        {
            var selectedFlight = _flightsRepository.Get(id);
            var citizen = _userService.GetUser();

            citizen.Flights.Add(selectedFlight);
            selectedFlight.Citizens.Add(citizen);

            _citizenRepository.Save(citizen);
            _flightsRepository.Save(selectedFlight);
            //Passenger passenger = new Passenger
            //{
            //    Flight = selectedFlight,
            //    Citizen = citizen
            //};
            //_passengersRepository.Save(passenger);
            Debug.WriteLine("Booked");
        }

        public void ConvertFlights(List<Flight> flights)
        {
            Random random = new Random();
            foreach (var flight in flights)
            {
                if (flight.FlightType == FlightType.IncomingFlight)
                {
                    flight.FlightType = FlightType.DepartingFlight;
                    flight.FlightStatus = FlightStatus.OnTime;
                }
                else
                {
                    flight.FlightType = FlightType.IncomingFlight;
                    flight.FlightStatus = FlightStatus.Expected;
                }
                flight.Date = DateTime.Now.AddDays(random.Next(5));
                _flightsRepository.Save(flight);
            }
        }
    }
}
