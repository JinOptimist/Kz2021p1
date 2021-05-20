using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Presentation.FirePresentation;

namespace WebApplication1.Controllers.Firemen
{
    public class FiremanTeamController : Controller
    {
        private IFiremanTeamPresentation _firemanTeamPresentation { get; set; }

        public FiremanTeamController(IFiremanTeamPresentation firemanTeamPresentation)
        {
            _firemanTeamPresentation = firemanTeamPresentation;
        }

        public IActionResult Index()
        {
            var viewModels = _firemanTeamPresentation.GetAllTeams();

            return View(viewModels);
        }
        [HttpGet]
        public IActionResult CreateFiremanTeam()
        {
            var model = new FiremanTeamViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateFiremanTeam(FiremanTeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _firemanTeamPresentation.CreateFiremanTeam(model);

            return RedirectToAction("Index", "FiremanTeam");
        }
        public JsonResult Remove(long id)
        {
            return Json(_firemanTeamPresentation.Remove(id));
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _firemanTeamPresentation.GetTeam(id);

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FiremanTeamViewModel model)
        {
            _firemanTeamPresentation.Edit(model);

            return RedirectToAction("Index", "FiremanTeam");
        }

    }
}
