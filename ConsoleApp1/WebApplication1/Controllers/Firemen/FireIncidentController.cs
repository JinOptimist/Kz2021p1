using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Controllers.Firemen
{
    public class FireIncidentController : Controller
    {
        private FireIncidentRepository _fireIncidentRepository { get; set; }
        private FiremanTeamRepository _firemanTeamRepository { get; set; }

        public FireIncidentController (FireIncidentRepository fireIncidentRepository, FiremanTeamRepository firemanTeamRepository)
        {
            _fireIncidentRepository = fireIncidentRepository;
            _firemanTeamRepository = firemanTeamRepository;
        }

        public IActionResult Index()
        {
            var viewmodels = _fireIncidentRepository.GetAll()
                .Select(x => new FireIncidentViewModel()
                {
                   Id = x.Id,
                   Address = x.Address,
                   Date = x.Date,
                   Reason = x.Reason,
                   Injured = x.Injured,
                   Dead = x.Dead,
                   TeamName = x.FiremanTeam.TeamName
                }
                ).ToList();
            return View(viewmodels);
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
            var newModel = new FireIncident()
            {
                Address = model.Address,
                Date = model.Date,
                Reason = model.Reason,
                Injured = model.Injured,
                Dead = model.Dead
            };
          
            var team = _firemanTeamRepository.GetByName(model.TeamName);
            newModel.FiremanTeam = team;
            
            _fireIncidentRepository.Save(newModel);
            return RedirectToAction("Index", "FireIncident");
        }
        public JsonResult Remove(long id)
        {
            var model = _fireIncidentRepository.Get(id);
            if (model == null)
            {
                return Json(false);
            }
            _fireIncidentRepository.Remove(model);
            return Json(true);
        }

    }
}
