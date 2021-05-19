using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.Controllers.BusSystem
{
    public class TripRouteController : Controller
    {
        private ITripRouteRepository _tripRouteRepository;
        private IBusRepository _busRepository;

        public TripRouteController(ITripRouteRepository tripRouteRepository, IBusRepository busRepository)
        {
            _tripRouteRepository = tripRouteRepository;
            _busRepository = busRepository;
        }

        public IActionResult Index()
        {
            var viewModels = _tripRouteRepository.GetAll()
                .Select(x => new TripViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Type = x.Type,
                    Length = x.Length,
                    Price = x.Price,
                    Buses = x.Buses
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult RoutesMap()
        {


            return View();
        }

        [HttpPost]
        public IActionResult CreateRoute(TripViewModel newRoute)
        {


            var route = new TripRoute()
            {
                Title = newRoute.Title,
                Type = newRoute.Type,
                Length = newRoute.Length,
                Price = newRoute.Price
            };

            _tripRouteRepository.Save(route);

            return RedirectToAction("Index");
        }


        public JsonResult Remove(long id)
        {


            var route = _tripRouteRepository.GetById(id);
            if (route == null)
            {
                return Json(false);
            }

            _busRepository.UpdateBusRoute(route);
            _tripRouteRepository.Remove(route);

            return Json(true);
        }
    }

}
