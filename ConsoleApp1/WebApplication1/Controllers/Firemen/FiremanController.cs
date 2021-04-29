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
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FiremanController : Controller
    {
        private FiremanRepository _firemanRepository { get; set; }
        private CitizenRepository _citizenRepository { get; set; }
        private FiremanTeamRepository _firemanTeamRepository { get; set; }
        private UserService _userService;

        private IMapper _mapper { get; set; }
        public FiremanController(FiremanRepository workerRepository, IMapper mapper, CitizenRepository citizenRepository, FiremanTeamRepository firemanTeamRepository, UserService userService)
        {
            _firemanRepository = workerRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _userService = _userService;
        }

        public IActionResult Index()
        {

            var viewModels = _firemanRepository
               .GetAll()
               .Select(x => new FiremanShowViewModel()
               {
                   Id = x.Id,
                   Age = x.Citizen.Age,
                   Name = x.Citizen.Name,
                   Role = x.Role,
                   WorkExperYears = x.WorkExperYears,
                   TeamName = x.FiremanTeam?.TeamName
               }).ToList();
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
            var citizen = _citizenRepository.GetByName(model.Name);

            var m = _mapper.Map<Fireman>(model);
            m.Citizen = citizen;
            m.CitizenId = citizen.Id;

            var fireteam = _firemanTeamRepository.GetByName(model.TeamName);
            m.FiremanTeam = fireteam;
            _firemanRepository.Save(m);
            return RedirectToAction("Index", "Fireman");
        }
        public IActionResult Main()
        {
            return View();
        }
        public JsonResult Remove(long id)
        {

            var fireman = _firemanRepository.Get(id);
            if (fireman == null)
            {
                return Json(false);
            }

            _firemanRepository.Remove(fireman);

            return Json(true);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var fireman = _firemanRepository.Get(id);
            var model = _mapper.Map<FiremanViewModel>(fireman);
           
            model.Name = fireman.Citizen.Name;
            model.TeamName = fireman.FiremanTeam?.TeamName;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FiremanViewModel model)
        {
           var fireman = _firemanRepository.Get(model.Id);
            if (fireman != null)
            {              
                fireman.WorkExperYears = model.WorkExperYears;
                fireman.Role = model.Role;
                fireman.FiremanTeam = _firemanTeamRepository.GetByName(model.TeamName);
                _firemanRepository.Save(fireman);
            }
            return RedirectToAction("Index", "FiremanTeam");
        } 
    }
}
