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
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Services;
using AutoMapper;

namespace WebApplication1.Controllers.BusSystem
{
    public class BusController : Controller
    {
        private IBusRepository _busRepository;
        private ITripRouteRepository _tripRouteRepository;
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }      

        public BusController(IBusRepository busRepository, ITripRouteRepository tripRouteRepository, IMapper mapper, IUserService userService)
        {
            _busRepository = busRepository;
            _tripRouteRepository = tripRouteRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("/cbs/main")]
        public IActionResult CityBusSystemMainPage()
        {
            ViewData["userName"] = _userService.GetUser().Name;
            return View();
        }

        [HttpGet("/cbs/index")]
        public IActionResult Index()
        {
            var viewModels = _busRepository.GetAll()
                .Select(x => new BusParkViewModel()
                {
                    Id = x.Id,
                    Model = x.Model,
                    Type = x.Type,
                    Capacity = x.Capacity,
                    Price = x.Price,
                    RoutePlan = x.RoutePlan,
                    IsOnOrder = x.IsOnOrder,
                }).ToList();

            ViewData["BusAmount"] = viewModels.Count;
            return View(viewModels);
        }

        [HttpGet("/cbs/bus_park")]
        public IActionResult BusParkPage()
        {
            return View();
        }

        [HttpGet("/cbs/routes_map")]
        public IActionResult RoutesMapPage()
        {
            return View();
        }

        [HttpGet("/cbs/bus_adding")]
        public IActionResult BusPark()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBus(BusParkViewModel newBus)
        {
            string busType = newBus.Type switch
            {
                "school" => "special",
                "administration" => "special",
                "special" => "special",
                "city" => "ordinary",
                "ordinary" => "ordinary",
                "touristic" => "touristic",
                _ => "special"
            };

            var bus = new Bus()
            {
                Model = newBus.Model,
                Type = newBus.Type,
                Capacity = newBus.Capacity,
                Price = newBus.Price,
                RoutePlan = _tripRouteRepository.GetByType(busType)
            };

            _busRepository.Save(bus);

            return RedirectToAction("Index");
        }

        public JsonResult Remove(long id)
        {
            var bus = _busRepository.GetById(id);
            if (bus == null)
            {
                return Json(false);
            }

            _busRepository.Remove(bus);

            return Json(true);
        }
    }
}
