using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HCWorkerController : Controller
    {
        private IHCWorkerRepository _hcworkerRepository;
        private IMapper _mapper;
        private ICitizenRepository _citizenRepository;
        private IHCEstablishmentsRepository _hcestablishmentsRepository;
        private IUserService _userService;
        public HCWorkerController(
            IHCWorkerRepository workerRepository, 
            IMapper mapper, 
            IHCEstablishmentsRepository facilityRepository, 
            ICitizenRepository citizenRepository,
            IUserService userService)
        {
            _hcworkerRepository = workerRepository;
            _mapper = mapper;
            _hcestablishmentsRepository = facilityRepository;
            _citizenRepository = citizenRepository;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var viewModels = _hcworkerRepository
              .GetAll()
              .Select(x => _mapper.Map<HCWorkerViewModel>(x)).ToList();
            return View(viewModels);
        }


        [IsHCWorker]
        [Authorize]
        [HttpGet]
        public IActionResult AddWorker()
        {
            var viewmodel = new HCWorkerViewModel();

            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult AddWorker(HCWorkerViewModel newWorker)
        {
            var model = _mapper.Map<HCWorker>(newWorker);
            var citizen = _citizenRepository.Get(newWorker.CitizenId);
            var facility = _hcestablishmentsRepository.Get(newWorker.FacilityId);

            model.CitizenId = citizen.Id;
            model.Name = citizen.Name;
            model.FacilityId = facility.Id;
            model.FacilityName = facility.Name;

            _hcworkerRepository.Save(model);

            return RedirectToAction("Index");
        }

        [IsHCWorker]
        [Authorize]
        public JsonResult Remove(long id)
        {
            var userForDelete = _hcworkerRepository.Get(id);

            if (userForDelete == null)
            {
                return Json(false);
            }

            _hcworkerRepository.Remove(userForDelete);

            return Json(true);
        }
    }
}
