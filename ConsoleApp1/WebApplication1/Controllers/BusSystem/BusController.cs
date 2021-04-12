using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace WebApplication1.Controllers.BusSystem
{
    public class BusController : Controller
    {
        private BusRepository _busRepository;

        public BusController(BusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public IActionResult CityBusSystemMainPage()
        {            
            return View();
        }

        public IActionResult Index()
        {
            var viewModels = _busRepository.GetAll()
                .Select(x => new BusParkViewModel()
                {
                    Model = x.Model,
                    Type = x.Type,
                    Capacity = x.Capacity,
                    Price = x.Price
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult BusPark()
        {
            var model = new BusParkViewModel()
            {
                Model = "ikar",
                Type = "ordinary",
                Capacity = 100,
                Price = 10
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBus(BusParkViewModel newBus)
        {
            

            var bus = new Bus()
            {
                Model = newBus.Model,
                Type = newBus.Type,
                Capacity = newBus.Capacity,
                Price = newBus.Price
            };
            
            _busRepository.Save(bus);

            return RedirectToAction("Index");
        }

        public JsonResult Remove(string model)
        {


            var bus = _busRepository.GetByModel(model);
            if (bus == null)
            {
                return Json(false);
            }

            _busRepository.Remove(bus);

            return Json(true);
        }

        // GET: BusParkViewModels/Edit/5
        


    }
    
}


