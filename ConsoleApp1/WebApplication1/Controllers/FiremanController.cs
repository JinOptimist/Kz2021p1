using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FiremanController : Controller
    {
        private FiremanRepository _firemanRepository { get; set; }
        private CitizenRepository _citizenRepository { get; set; }
        private IMapper _mapper { get; set; }
        public FiremanController(FiremanRepository workerRepository, IMapper mapper, CitizenRepository citizenRepository)
        {
            _firemanRepository = workerRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
        }

        public IActionResult Index()
        {
            var viewModels = _firemanRepository
               .GetAll()
               .Select(x => new FiremanShowViewModel()
               {
                   Id = x.Id,
                   Age = x.Citizen.Age,//_citizenRepository.Get(x.CitizenId).Age,
                   Name = x.Citizen.Name,
                   Role = x.Role,
                   WorkExperYears = x.WorkExperYears
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

    }
}
