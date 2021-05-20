using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.BusSystem
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private IBusRepository _busRepository;
        private ITripRouteRepository _tripRouteRepository;
        private IUserService _userService { get; set; }
        private IMapper _mapper { get; set; }

        public OrderController(IOrderRepository orderRepository, IBusRepository busRepository, ITripRouteRepository tripRouteRepository, IUserService userService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _busRepository = busRepository;
            _tripRouteRepository = tripRouteRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Acknow()
        {
            ViewData["userName"] = _userService.GetUser().Name;
            return View();
        }

        public IActionResult OrderPage()
        {
            return View();
        }

        [HttpGet("/cbs/orders/index")]
        public IActionResult Index()
        {
            var viewModels = _orderRepository.GetAll()
                .Select(x => new OrderViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Period = x.Period,
                    RouteTitle = x.RouteTitle,
                    FinalPrice = x.FinalPrice,
                }).ToList();
            return View(viewModels);
        }

        [HttpGet("/cbs/orders/error")]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MakeOrder()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult MakeOrder(OrderViewModel newOrder)
        {
            if (_busRepository.IsFree(newOrder.Model, newOrder.RouteTitle))
            {
                var busPrice = _busRepository.GetByModel(newOrder.Model).Price;
                var routePrice = _tripRouteRepository.GetByTitle(newOrder.RouteTitle).Price;
                var order = new Order()
                {
                    Name = newOrder.Name,
                    Model = newOrder.Model,
                    Period = newOrder.Period,
                    RouteTitle = newOrder.RouteTitle,
                    FinalPrice = Convert.ToDouble(busPrice + routePrice)
                };
                                
                long freeBusId = _busRepository.FreeBusId(newOrder.Model, newOrder.RouteTitle);
                _busRepository.UpdateBusOrderStatus(freeBusId, true);
                _orderRepository.Save(order);

                return RedirectToAction("Acknow");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public JsonResult Remove(long id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return Json(false);
            }

            _orderRepository.Remove(order);

            return Json(true);
        }
    }
}