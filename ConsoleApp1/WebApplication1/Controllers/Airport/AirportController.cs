using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Models.Airport;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        private UserService _userService { get; set; }
        private IMapper _mapper { get; set; }
        private CitizenRepository _citizenRepository { get; set; }
        private FlightsRepository _flightsRepository { get; set; }
        private PassengersRepository _passengersRepository { get; set; }

        public AirportController(UserService userService, IMapper mapper, CitizenRepository citizenRepository, FlightsRepository flightsRepository, PassengersRepository passengersRepository)
        {
            _userService = userService;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
            _flightsRepository = flightsRepository;
            _passengersRepository = passengersRepository;
        }

        public IActionResult Index()
        {
            // TODO: filter flights by departure time
            List<IncomingFlightInfoViewModel> incomingFlightsInfo = _flightsRepository.GetAll().Where(flight => flight.FlightType == FlightType.IncomingFlight).Select(flightInfo => _mapper.Map<IncomingFlightInfoViewModel>(flightInfo)).ToList();
            return View(incomingFlightsInfo);
        }

        public IActionResult AvailableFlights()
        {
            List<Flight> departingFlightsAvailableForBooking = _flightsRepository.GetAll().Where(flight => flight.FlightStatus == FlightStatus.OnTime && flight.FlightType == FlightType.DepartingFlight).ToList();
            return View(departingFlightsAvailableForBooking);
        }
        public IActionResult BookTicket(long id)
        {
            Citizen citizen = _userService.GetUser();
            if (citizen == null)
            {
                return RedirectToAction("Login", "Citizen");
            }
            Flight selectedFlight = _flightsRepository.GetAll().SingleOrDefault(f => f.Id == id && f.FlightType == FlightType.DepartingFlight);
            if (selectedFlight == null)
            {
                return RedirectToAction("AvailableFlights");
            }
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
            return View("./Views/Airport/BookingConfirmation.cshtml");
        }
        private bool DepartureIsNow(Flight selectedFlight)
        {
            DateTime now = DateTime.Now;
            DateTime dt = selectedFlight.Date;
            if (dt.Day == now.Day && dt.AddHours(-1).Hour <= now.Hour && now.Hour <= dt.Hour)
                return true;
            return false;
        }
    }
}
