using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        private IncomingFlightsRepository _incomingFlightsRepository;
        private DepartingFlightsRepository _departingFlightsRepository;

        public AirportController(IncomingFlightsRepository incomingFlightsRepository, DepartingFlightsRepository departingFlightsRepository)
        {
            _incomingFlightsRepository = incomingFlightsRepository;
            _departingFlightsRepository = departingFlightsRepository;
        }

        public IActionResult Index()
        {
            List<IncomingFlightInfo> incomingFlightsInfo = _incomingFlightsRepository.GetAll();
            return View(incomingFlightsInfo);
        }

        [HttpGet]
        public IActionResult AvailableFlights()
        {
            List<DepartingFlightInfo> departingFlightsAvailableForBooking = _departingFlightsRepository.GetAll().Where(flight => flight.Status == "On Time").ToList();
            return View(departingFlightsAvailableForBooking);
        }
    }
}
