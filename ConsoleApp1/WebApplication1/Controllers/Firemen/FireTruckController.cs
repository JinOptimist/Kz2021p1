using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Presentation.FirePresentation;

namespace WebApplication1.Controllers.Firemen
{
    public class FireTruckController : Controller
    {
        private IFireTruckPresentation _fireTruckPresentation { get; set; }
        public FireTruckController(IFireTruckPresentation fireTruckPresentation)
        {
            _fireTruckPresentation = fireTruckPresentation;
        }

        public IActionResult Index()
        {
            var models = _fireTruckPresentation.GetAllTrucks();

            return View(models);
        }
        [HttpGet]
        public IActionResult AddFireTruck()
        {
            var model = new FireTruckViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddFireTruck(FireTruckViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _fireTruckPresentation.AddFireTruck(model);

            return RedirectToAction("Index", "FireTruck");
        }
        public JsonResult Remove(long id)
        {
            return Json(_fireTruckPresentation.Remove(id));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _fireTruckPresentation.GetTruck(id);

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FireTruckViewModel model)
        {
            _fireTruckPresentation.Edit(model);

            return RedirectToAction("Index", "FireTruck");
        }
    }
}
