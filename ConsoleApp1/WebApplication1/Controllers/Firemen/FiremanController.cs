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
            _userService = userService;
        }

        public IActionResult Index()
        {

            var viewModels = _firemanRepository
               .GetAll()
               .Select(x => _mapper.Map<FiremanViewModel>(x)).ToList();

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
            model.Age = fireman.Citizen.Age;

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FiremanViewModel model)
        {
           var fireman = _firemanRepository.Get(model.Id);
            if (fireman != null)
            {
                if (!_userService.IsFireAdmin())
                {
                    var citizen = _citizenRepository.Get(fireman.CitizenId);
                    citizen.Name = model.Name;
                    citizen.Age = model.Age;
                    fireman.WorkExperYears = model.WorkExperYears;
                    _firemanRepository.Save(fireman);
                    _citizenRepository.Save(citizen);

                    return RedirectToAction("MyProfile", "Fireman");
                }
                else
                {
                    fireman.Role = model.Role;
                    fireman.FiremanTeam = _firemanTeamRepository.GetByName(model.TeamName);
                    _firemanRepository.Save(fireman);                    
                }                
            }
            return RedirectToAction("Index", "Fireman");
        }
        [HttpGet]
        [Authorize]
        public IActionResult MyProfile()
        {
            var user = _userService.GetUser();
            var fireman = _firemanRepository.GetByName(user.Name);
            if (fireman != null)
            {
                var viewModel = _mapper.Map<FiremanViewModel>(fireman);
                return View(viewModel);
            }

            return RedirectToAction("Main", "Fireman");
        }
    }
}
