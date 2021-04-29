using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;

namespace WebApplication1.Controllers.BusSystem
{
    public class TripRouteController : Controller
    {
        private TripRouteRepository _tripRouteRepository;

        public TripRouteController(TripRouteRepository tripRouteRepository)
        {
            _tripRouteRepository = tripRouteRepository;
        }

        public IActionResult Index()
        {
            var viewModels = _tripRouteRepository.GetAll()
                .Select(x => new TripViewModel()
                {
                    Title = x.Title,
                    Type = x.Type,
                    Length = x.Length,
                    Price = x.Price
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult RoutesMap()
        {
            var model = new TripViewModel()
            {
                Title = "ikar",
                Type = "ordinary",
                Length = 100,
                Price = 10
            };

            return View(model);
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

        public JsonResult Remove(string title)
        {


            var route = _tripRouteRepository.GetByTitle(title);
            if (route == null)
            {
                return Json(false);
            }

            _tripRouteRepository.Remove(route);

            return Json(true);
        }
    }
    
}


