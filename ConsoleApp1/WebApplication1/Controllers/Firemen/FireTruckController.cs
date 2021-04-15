using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Controllers.Firemen
{
    public class FireTruckController : Controller
    {
        private FireTruckRepository _fireTruckRepository { get; set; }
        private FiremanTeamRepository _firemanTeamRepository { get; set; }

        public IActionResult Index()
        {
            var viewmodels = _fireTruckRepository.GetAll()
                .Select(x => new FireTruckViewModel()
                {
                    Id = x.Id,
                    TruckNumber = x.TruckNumber,
                    TruckState = x.TruckState,
                    TeamName = x.FiremanTeam.TeamName
                }
                );
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
            var team = _firemanTeamRepository.GetByName(model.TeamName);

            var newModel = new FireTruck()
            {
                TruckNumber = model.TruckNumber,
                TruckState = model.TruckState,
                FiremanTeam = team
            };

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
    }
}
