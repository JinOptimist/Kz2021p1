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
        private FiremanRepository _workerRepository { get; set; }
        private CitizenRepository _citizenRepository { get; set; }
        private IMapper _mapper { get; set; }
        public FiremanController(FiremanRepository workerRepository, IMapper mapper, CitizenRepository citizenRepository)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
        }
        
        public IActionResult Index()
        {
            var models = _workerRepository.GetAll()
                   .Select(x => _mapper.Map<FiremanViewModel>(x))
                   .ToList();

            return View(models);
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
           // var citizen = _citizenRepository.GetByName(model.NameFireman);
            var m = _mapper.Map<Fireman>(model);
          //  m.Citizen_ = citizen;
            _workerRepository.Save(m);
            return RedirectToAction("Index", "Fireman");
        }
        public IActionResult Main()
        {
            return View();
        }
        public JsonResult Remove(long id)
        {      

            var fireman = _workerRepository.Get(id);
            if (fireman == null)
            {
                return Json(false);
            }

            _workerRepository.Remove(fireman);

            return Json(true);
        }

    }
}
