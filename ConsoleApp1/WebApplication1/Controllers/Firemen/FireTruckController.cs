using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Controllers.Firemen
{
    public class FireTruckController : Controller
    {
        private FireTruckRepository _fireTruckRepository { get; set; }
        private FiremanTeamRepository _firemanTeamRepository { get; set; }
        private IMapper _mapper { get; set; }
        public FireTruckController(FiremanTeamRepository firemanTeamRepository, FireTruckRepository fireTruckRepository, IMapper mapper)
        {
            _fireTruckRepository = fireTruckRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewmodels = _fireTruckRepository.GetAll()
                .Select(x => new FireTruckViewModel()
                {
                    Id = x.Id,
                    TruckNumber = x.TruckNumber,
                    TruckState = x.TruckState,
                    TeamName = x.FiremanTeam?.TeamName
                }
                ).ToList();
            return View(viewmodels);
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
            var newModel = new FireTruck()
            {
                TruckNumber = model.TruckNumber,
                TruckState = model.TruckState,                
            };
            if (model.TeamName == "0")
            {
                newModel.FiremanTeam = null;
            }
            else
            {
                var team = _firemanTeamRepository.GetByName(model.TeamName);
                newModel.FiremanTeam = team;
            }
            _fireTruckRepository.Save(newModel);
            return RedirectToAction("Index", "FireTruck");
        }
        public JsonResult Remove(long id)
        {
            var model = _fireTruckRepository.Get(id);
            if (model == null)
            {
                return Json(false);
            }
            _fireTruckRepository.Remove(model);
            return Json(true);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var firetruck = _fireTruckRepository.Get(id);
            var model = _mapper.Map<FireTruckViewModel>(firetruck);

            model.TeamName = firetruck.FiremanTeam?.TeamName;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FireTruckViewModel model)
        {
            var firetruck = _fireTruckRepository.Get(model.Id);
            if (firetruck != null)
            {
                firetruck.TruckNumber = model.TruckNumber;
                firetruck.TruckState = model.TruckState;
                firetruck.FiremanTeam = _firemanTeamRepository.GetByName(model.TeamName);
               
                _fireTruckRepository.Save(firetruck);
            }
            return RedirectToAction("Index", "FireTruck");
        }
    }
}
