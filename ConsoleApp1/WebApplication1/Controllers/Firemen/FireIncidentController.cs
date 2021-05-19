using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Presentation.FirePresentation;

namespace WebApplication1.Controllers.Firemen
{
    public class FireIncidentController : Controller
    {
        private IFireIncidentPresentation _fireIncidentPresentation { get; set; }

        public FireIncidentController(IFireIncidentPresentation fireIncidentPresentation)
        {
            _fireIncidentPresentation = fireIncidentPresentation;
        }

        public IActionResult Index()
        {
            var viewModels = _fireIncidentPresentation.GetAllIncidents();

            return View(viewModels);
        }
        [HttpGet]
        public IActionResult AddFireIncident()
        {
            var model = new FireIncidentViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddFireIncident(FireIncidentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _fireIncidentPresentation.AddFireIncident(model);

            return RedirectToAction("Main", "Fireman");
        }
        public JsonResult Remove(long id)
        {
            return Json(_fireIncidentPresentation.Remove(id));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _fireIncidentPresentation.GetIncident(id);

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FireIncidentViewModel model)
        {
            _fireIncidentPresentation.Edit(model);

            return RedirectToAction("Index", "FireIncident");
        }
        [HttpGet]
        public IActionResult FireCall()
        {
            var model = new FireIncidentViewModel();
            return View(model);
        }
    }
}
