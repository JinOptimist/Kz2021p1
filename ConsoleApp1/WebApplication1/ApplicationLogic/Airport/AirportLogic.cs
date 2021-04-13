using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Services;

namespace WebApplication1.ApplicationLogic.Airport
{
    public class AirportLogic
    {
        private PassengersRepository _passengersRepository { get; set; }
        private FlightsRepository _flightsRepository { get; set; }
        public CitizenRepository _citizenRepository { get; set; }
        public UserService _userService { get; set; }

        public AirportLogic(PassengersRepository passengersRepository, CitizenRepository citizenRepository, UserService userService, FlightsRepository flightsRepository)
        {
            _passengersRepository = passengersRepository;
            _citizenRepository = citizenRepository;
            _userService = userService;
            _flightsRepository = flightsRepository;
        }

        public void AdmitPassengers()
        {
            List<Flight> arrivedFlights = new List<Flight>();
            foreach (var passenger in _passengersRepository.GetAll().Where(passenger => passenger.Flight.FlightType == FlightType.IncomingFlight && IsValidAddmissionTime(passenger.Flight.Date)))
            {
                if (!arrivedFlights.Contains(passenger.Flight))
                {
                    arrivedFlights.Add(passenger.Flight);
                }
                passenger.Citizen.IsOutOfCity = false;
                _passengersRepository.Save(passenger);
                _passengersRepository.Remove(passenger);
            }
            ConvertFlights(arrivedFlights);
        }

        public void DepartPassengers()
        {
            List<Flight> departedFlights = new List<Flight>();
            foreach (var passenger in _passengersRepository.GetAll().Where(passenger => passenger.Flight.FlightType == FlightType.DepartingFlight && IsValidDepartureTime(passenger.Flight.Date)))
            {
                if (!departedFlights.Contains(passenger.Flight))
                {
                    departedFlights.Add(passenger.Flight);
                }
                passenger.Citizen.IsOutOfCity = true;
                _citizenRepository.Save(passenger.Citizen);
            }
            ConvertFlights(departedFlights);
        }

        public bool FlightIsValid(long id)
        {
            Flight selectedFlight = _flightsRepository.GetAll().SingleOrDefault(f => f.Id == id && f.FlightType == FlightType.DepartingFlight);
            return selectedFlight != null;
        }

        public void BookTicket(long id)
        {
            Flight selectedFlight = _flightsRepository.Get(id);
            Citizen citizen = _userService.GetUser();

            Passenger passenger = new Passenger
            {
                FlightId = selectedFlight.Id,
                CitizenId = citizen.Id
            };
            _passengersRepository.Save(passenger);
        }

        public bool IsValidDepartureTime(DateTime flightDate)
        {
            DateTime now = DateTime.Now;
            if (flightDate.Day == now.Day && flightDate.AddMinutes(-30) <= now && now >= flightDate)
                return true;
            return false;
        }

        public bool IsValidAddmissionTime(DateTime flightDate)
        {
            DateTime now = DateTime.Now;
            DateTime add30 = flightDate.AddMinutes(30);
            if (flightDate.Day == now.Day && now >= flightDate && now <= add30)
                return true;
            return false;
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
