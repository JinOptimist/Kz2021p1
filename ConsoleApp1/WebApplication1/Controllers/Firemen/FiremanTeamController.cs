using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
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
        public FiremanTeamController(FiremanTeamRepository firemanTeamRepository)
        {
            _firemanTeamRepository = firemanTeamRepository;
        }

        public IActionResult Index()
        {
            var viewModels = _firemanTeamRepository.GetAll()
                .Select(x => new FiremanTeamViewModel()
                {
                    Id = x.Id,
                    TeamName = x.TeamName,
                    TruckId = x.TruckId,
                    Shift = x.Shift,
                    TeamState = x.TeamState,
                    TruckState = x.FireTruck.TruckState,
                    FiremanCount = x.Firemen.Count()
                }).ToList();
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
            var newModel = new FiremanTeam()
            {
                TeamName = model.TeamName,
                TruckId = model.TruckId,
                Shift = model.Shift,
                TeamState = model.TeamState
            };
            var truck = _fireTruckRepository.Get(model.TruckId);
            newModel.FireTruck = truck;
            foreach (var fireman in model.FiremenNames)
            {
                newModel.Firemen.Add(_firemanRepository.GetByName(fireman));
            }
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

        }
}
