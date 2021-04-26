using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.Models.Airport;
using WebApplication1.Presentation.Airport;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        private AirportPresentation _airpotPresentation { get; set; }

        public AirportController(AirportPresentation airpotPresentation)
        {
            _airpotPresentation = airpotPresentation;
        }

        public IActionResult Index()
        {
            List<IncomingFlightInfoViewModel> incomingFlightsInfo = _airpotPresentation.GetIndexViewModel();
            _airpotPresentation.AdmitPassengers();
            return View(incomingFlightsInfo);
        }

        public IActionResult AvailableFlights()
        {
            List<Flight> departingFlightsAvailableForBooking = _airpotPresentation.GetAvailableFlights();
            _airpotPresentation.DepartPassengers();
            return View(departingFlightsAvailableForBooking);
        }

        [Authorize]
        public IActionResult BookTicket(long id)
        {
            if (!_airpotPresentation.FlightIsValid(id)) return RedirectToAction("AvailableFlights");
            _airpotPresentation.BookTicket(id);
            return View("./Views/Airport/BookingConfirmation.cshtml");
        }
    }
}
