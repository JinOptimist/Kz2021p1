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
        private IncomingFlightsRepository _incomingFlightsRepository { get; set; }
        private DepartingFlightsRepository _departingFlightsRepository { get; set; }
        private UserService _userService { get; set; }
        private PassengersRepository _passengersRepository { get; set; }
        private IMapper _mapper { get; set; }
        private CitizenRepository _citizenRepository { get; set; }

        public AirportController(IncomingFlightsRepository incomingFlightsRepository, DepartingFlightsRepository departingFlightsRepository, UserService userService, PassengersRepository passengersRepository, IMapper mapper, CitizenRepository citizenRepository)
        {
            _incomingFlightsRepository = incomingFlightsRepository;
            _departingFlightsRepository = departingFlightsRepository;
            _userService = userService;
            _passengersRepository = passengersRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
        }

        public IActionResult Index()
        {
            // TODO: filter flights by departure time
            List<IncomingFlightInfoViewModel> incomingFlightsInfo = _incomingFlightsRepository.GetAll().Select(flightInfo => _mapper.Map<IncomingFlightInfoViewModel>(flightInfo)).ToList();
            return View(incomingFlightsInfo);
        }

        public IActionResult AvailableFlights()
        {
            List<DepartingFlightInfo> departingFlightsAvailableForBooking = _departingFlightsRepository.GetAll().Where(flight => flight.Status == "On Time").ToList();
            return View(departingFlightsAvailableForBooking);
        }
        public IActionResult BookTicket(long id)
        {
            Citizen citizen = _userService.GetUser();
            if (citizen == null)
            {
                return RedirectToAction("Login", "Citizen");
            }
            DepartingFlightInfo selectedFlight = _departingFlightsRepository.Get(id);
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
            if (selectedFlight.DepartureIsNow())
            {
                citizen.IsOutOfCity = true;
                _citizenRepository.Save(citizen);
            }
            return View("./Views/Airport/BookingConfirmation.cshtml");
        }
    }
}
