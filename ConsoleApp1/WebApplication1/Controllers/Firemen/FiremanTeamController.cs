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
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Controllers.Firemen
{
    public class FiremanTeamController : Controller
    {
        private FiremanTeamRepository _firemanTeamRepository { get; set; }
        private FireTruckRepository _fireTruckRepository { get; set; }
        private FiremanRepository _firemanRepository { get; set; }
        private IMapper _mapper { get; set; }
      

        public FiremanTeamController(FiremanTeamRepository firemanTeamRepository, FireTruckRepository fireTruckRepository, FiremanRepository firemanRepository, IMapper mapper)
        {
            _firemanTeamRepository = firemanTeamRepository;
            _fireTruckRepository = fireTruckRepository;
            _firemanRepository = firemanRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModels = _firemanTeamRepository.GetAll()
                .Select(x => _mapper.Map<FiremanTeamViewModel>(x)).ToList();
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
            var newModel = _mapper.Map<FiremanTeam>(model);

            var truck = _fireTruckRepository.Get(model.TruckId);
           
            newModel.FireTruck = truck;
            
            truck.FiremanTeam = newModel;
            _firemanTeamRepository.Save(newModel);
            return RedirectToAction("Index", "FiremanTeam");
        }
        public JsonResult Remove(long id)
        {
            var team = _firemanTeamRepository.Get(id);
            if (team == null)
            {
                return Json(false);
            }
            _firemanTeamRepository.Remove(team);
            return Json(true);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var firemanteam = _firemanTeamRepository.Get(id);
            var model = _mapper.Map<FiremanTeamViewModel>(firemanteam);

            model.TruckState = firemanteam.FireTruck.TruckState;
            model.FiremanCount = firemanteam.Firemen.Count();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FiremanTeamViewModel model)
        {
            var firemanteam = _firemanTeamRepository.Get(model.Id);
            if (firemanteam != null)
            {
                firemanteam.TeamName = model.TeamName;
                firemanteam.Shift = model.Shift;
                firemanteam.TeamState = model.TeamState;
                firemanteam.TruckId = model.TruckId;
                firemanteam.FireTruck = _fireTruckRepository.Get(model.TruckId);
                _firemanTeamRepository.Save(firemanteam);
            }
            return RedirectToAction("Index", "Fireman");
        }

    }
}
