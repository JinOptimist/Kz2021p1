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
        private IAirportPresentation _airpotPresentation { get; set; }

        public AirportController(IAirportPresentation airpotPresentation)
        {
            _airpotPresentation = airpotPresentation;
        }

        public IActionResult Index()
        {
            var incomingFlightsInfo = _airpotPresentation.GetIndexViewModel();
            return View(incomingFlightsInfo);
        }

        public IActionResult AvailableFlights()
        {
           var departingFlightsAvailableForBooking = _airpotPresentation.GetAvailableFlights();
            return View(departingFlightsAvailableForBooking);
        }

        [Authorize]
        public IActionResult BookTicket(long id)
        {
            if (!_airpotPresentation.FlightIsValid(id) || _airpotPresentation.FlightIsAlreadyBooked(id)) 
            {
                return RedirectToAction("AvailableFlights");
            }
            _airpotPresentation.BookTicket(id);
            return RedirectToAction("BookingConfirmation");
        }
        public IActionResult BookingConfirmation()
        {
            return View();
        }
    }
}
