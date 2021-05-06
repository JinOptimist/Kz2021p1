using AutoMapper;
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
        private IMapper _mapper { get; set; }

        public FireIncidentController (FireIncidentRepository fireIncidentRepository, FiremanTeamRepository firemanTeamRepository, IMapper mapper)
        {
            _fireIncidentRepository = fireIncidentRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewmodels = _fireIncidentRepository.GetAll()
                .Select(x => _mapper.Map<FireIncidentViewModel>(x)).ToList();
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
            var newModel = _mapper.Map<FireIncident>(model);     
          
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
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var fireincident = _fireIncidentRepository.Get(id);
            var model = _mapper.Map<FireIncidentViewModel>(fireincident);

            model.TeamName = fireincident.FiremanTeam?.TeamName;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FireIncidentViewModel model)
        {
            var incident = _fireIncidentRepository.Get(model.Id);
            if (incident != null)
            {
                incident.Address = model.Address;
                incident.Date = model.Date;
                incident.Reason = model.Reason;
                incident.Injured = model.Injured;
                incident.Dead = model.Dead;
                incident.Date = model.Date;

                var team = _firemanTeamRepository.GetByName(model.TeamName);

                incident.TeamId = team.Id;
                incident.FiremanTeam = team;

                _fireIncidentRepository.Save(incident);
            }
            return RedirectToAction("Index", "FireIncident");
        }
    }
}
