using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models;
using WebApplication1.Presentation.FirePresentation;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FiremanController : Controller
    {
        private IFiremanPresentation _firemanPresentation { get; set; }


        public FiremanController(IFiremanPresentation firemanPresentation)
        {
            _firemanPresentation = firemanPresentation;
        }

        public IActionResult Index()
        {
            var viewModels = _firemanPresentation.GetAllFiremen();

            return View(viewModels);
        }
        [HttpGet]
        public IActionResult CreateFireman()
        {
            var model = new FiremanViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateFireman(FiremanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _firemanPresentation.CreateFireman(model);
            return RedirectToAction("Index", "Fireman");
        }
        public IActionResult Main()
        {
            var models = _firemanPresentation.GetCurrentIncidents();
            return View(models);
        }
        public JsonResult Remove(long id)
        {
            return Json(_firemanPresentation.Remove(id));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _firemanPresentation.GetFireman(id);

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FiremanViewModel model)
        {
            string user = _firemanPresentation.Edit(model);
            if (user.Equals("fireadmin"))
            {
                return RedirectToAction("Index", "Fireman");
            }

            return RedirectToAction("MyProfile", "Fireman");
        }
        [HttpGet]
        [Authorize]
        public IActionResult MyProfile()
        {
            var model = _firemanPresentation.MyProfile();
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Main", "Fireman");
        }     
    }
}
