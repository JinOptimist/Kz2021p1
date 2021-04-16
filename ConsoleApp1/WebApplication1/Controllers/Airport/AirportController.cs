using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.ApplicationLogic.Airport;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.Models.Airport;
using WebApplication1.Presentation.Airport;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        private AirportLogic _airportLogic { get; set; }
        private AirportPresentation _airpotPresentation { get; set; }

        public AirportController(AirportLogic airportLogic, AirportPresentation airpotPresentation)
        {
            _airportLogic = airportLogic;
            _airpotPresentation = airpotPresentation;
        }

        public IActionResult Index()
        {
            List<IncomingFlightInfoViewModel> incomingFlightsInfo = _airpotPresentation.GetIndexViewModel();
            _airportLogic.AdmitPassengers();
            return View(incomingFlightsInfo);
        }

        public IActionResult AvailableFlights()
        {
            List<Flight> departingFlightsAvailableForBooking = _airpotPresentation.GetAvailableFlights();
            _airportLogic.DepartPassengers();
            return View(departingFlightsAvailableForBooking);
        }

        [Authorize]
        public IActionResult BookTicket(long id)
        {
            if (!_airportLogic.FlightIsValid(id)) return RedirectToAction("AvailableFlights");
            _airportLogic.BookTicket(id);
            return View("./Views/Airport/BookingConfirmation.cshtml");
        }
    }
}
