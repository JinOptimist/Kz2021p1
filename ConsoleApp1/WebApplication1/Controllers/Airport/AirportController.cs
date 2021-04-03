using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        private IncomingFlightsRepository _incomingFlightsRepository;
        private DepartingFlightsRepository _departingFlightsRepository;
        private UserService _userService;
        private PassengersRepository _passengersRepository;

        public AirportController(IncomingFlightsRepository incomingFlightsRepository, DepartingFlightsRepository departingFlightsRepository, UserService userService, PassengersRepository passengersRepository)
        {
            _incomingFlightsRepository = incomingFlightsRepository;
            _departingFlightsRepository = departingFlightsRepository;
            _userService = userService;
            _passengersRepository = passengersRepository;
        }

        public IActionResult Index()
        {
            List<IncomingFlightInfo> incomingFlightsInfo = _incomingFlightsRepository.GetAll();
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
            return View("./Views/Airport/BookingConfirmation.cshtml");
        }
    }
}
