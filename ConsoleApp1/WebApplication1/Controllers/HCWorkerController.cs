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
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HCWorkerController : Controller
    {
        private HCWorkerRepository _hcworkerRepository;
        private IMapper Mapper;
        private CitizenRepository _citizenRepository;
        private HCEstablishmentsRepository _hcestablishmentsRepository;
        private UserService _userService;
        public HCWorkerController(HCWorkerRepository workerRepository, IMapper mapper, HCEstablishmentsRepository facilityRepository, CitizenRepository citizenRepository)
        {
            _hcworkerRepository = workerRepository;
            Mapper = mapper;
            _hcestablishmentsRepository = facilityRepository;
            _citizenRepository = citizenRepository;
        }

        [IsHCWorker]
        [Authorize]
        public IActionResult Index()
        {
            var user = _userService.GetUser();
            var viewmodel = Mapper.Map<HCWorker>(user);

            return View(viewmodel);
        }
        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddWorker(HCWorkerViewModel newWorker)
        {
            var model = Mapper.Map<HCWorker>(newWorker);
            var citizen = _citizenRepository.Get(newWorker.CitizenId);
            var facility = _hcestablishmentsRepository.Get(newWorker.FacilityId);

            model.CitizenId = citizen.Id;
            model.Name = citizen.Name;
            model.FacilityId = facility.Id;
            model.FacilityName = facility.Name;

            _hcworkerRepository.Save(model);

            return RedirectToAction("Index");
        }

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
