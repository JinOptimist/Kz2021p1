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
            foreach (var passenger in _passengersRepository.GetAll().Where(passenger => passenger.Flight.FlightType == FlightType.IncomingFlight))
            {
                passenger.Citizen.IsOutOfCity = false;
                _passengersRepository.Save(passenger);
                _passengersRepository.Remove(passenger);
            }
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
            if (DepartureIsNow(selectedFlight))
            {
                citizen.IsOutOfCity = true;
                _citizenRepository.Save(citizen);
            }
        }


        public bool DepartureIsNow(Flight selectedFlight)
        {
            DateTime now = DateTime.Now;
            DateTime dt = selectedFlight.Date;
            if (dt.Day == now.Day && dt.AddHours(-1).Hour <= now.Hour && now.Hour <= dt.Hour)
                return true;
            return false;
        }
    }
}
