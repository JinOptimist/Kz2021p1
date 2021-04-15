using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.RestoBusiness;
using WebApplication1.EfStuff;

namespace WebApplication1.Controllers
{
    public class RestoransController : Controller
    {
        private KzDbContext _kzDbContext;
        private BronRestoBusiness _bronRestoBusiness;
        private BronRestoRepository _bronRestoRepository;
        private RestoransRepository _restoransRepository;
        private IMapper MapResto { get; set; }
        public RestoransController(RestoransRepository restoransRepository, IMapper mapper, BronRestoRepository bronRestoRepository,BronRestoBusiness bronRestoBusiness, KzDbContext kzDbContext)
        {
            _restoransRepository = restoransRepository;
            MapResto = mapper;
            _bronRestoRepository = bronRestoRepository;
            _bronRestoBusiness = bronRestoBusiness;
            _kzDbContext = kzDbContext;
        }
        public IActionResult AvailableResto()
        {
            var viewModels = _restoransRepository.GetAll()
                .Select(x => MapResto.Map<AvailableRestoModel>(x)).Where(x => x.Access == true && x.NumberOfTables != 0)
                .ToList();
            return View(viewModels);
        }

        public IActionResult Index()
        {

            var viewModels = _restoransRepository.GetAll()
                .Select(x => MapResto.Map<RestoViewModel>(x))
                .ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult CreateResto()
        {
            var model = new RestoViewModel() { };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateResto(RestoViewModel newResto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var resto = MapResto.Map<Restorans>(newResto);
            _restoransRepository.Save(resto);
            return RedirectToAction("Index");
        }

        public JsonResult Remove(string name)
        {
            var resto = _restoransRepository.GetByName(name);
            if (resto == null)
            {
                return Json(false);
            }
            _restoransRepository.Remove(resto);
            return Json(true);
        }

        [HttpPost]
        public IActionResult OneResto(string name)
        {
            var viewModel = _restoransRepository.GetByName(name);
            var viewModelOne = MapResto.Map<OneRestoViewModel>(viewModel);
            return View(viewModelOne);
        }

        [HttpPost]
        public IActionResult BronView(string name)
        {
            var restomodel = _restoransRepository.GetByName(name);
            var modelforBron = MapResto.Map<BronViewModel>(restomodel);
            if (restomodel.NumberOfTables-- == 0)
            {
                return RedirectToAction("AvailableResto", "Restorans");
            }
            return View(modelforBron);
        }

        [HttpPost]
        public IActionResult BronResto(BronViewModel model)
        {
            var bronrestModel=_bronRestoBusiness.BronR(model);
            _bronRestoRepository.Save(bronrestModel);
            return RedirectToAction("WaitConfirm", "Restorans", new { bronnumber = bronrestModel.BronRespNumber });
        }

        public IActionResult WaitConfirm(int bronnumber)
        {
            var brnumber = new BronNumberViewModel();
            brnumber.BronRespNumber = bronnumber;
            return View(brnumber);
        }

        [HttpPost]
        public IActionResult WaitAdminConfirm(BronNumberViewModel model)
        {
            var bbbrrr = _bronRestoRepository.GetByBrNumber(model.BronRespNumber);
            if (!bbbrrr.StateReservation)
            {
                return RedirectToAction("WaitConfirm", "Restorans", new { bronnumber = model.BronRespNumber });
            }
            return View(model);
        }
     


        public IActionResult NewRequestBron()
        {
            var viewModels = _bronRestoRepository.GetAll()
                .Select(x => MapResto.Map<BronAdminViewModel>(x)).Where(x=> !x.StateReservation)
                .ToList();
            return View(viewModels);
        }

        public IActionResult AdminConfirmBron(int numb)
        {
            var br = _bronRestoRepository.GetByBrNumber(numb);
            br.StateReservation = true;
            _bronRestoRepository.SaveChang(br);
            return RedirectToAction("NewRequestBron");
        }

        [HttpGet]
        public IActionResult CheckBron()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckBron(BronNumberViewModel vmodel)
        {
            return RedirectToAction("WaitAdminConfirm", new { model=vmodel });
        }

        public JsonResult RemoveBron(int number)
        {
            var bron = _bronRestoRepository.GetByBrNumber(number);
            if (bron == null)
            {
                return Json(false);
            }
            _bronRestoRepository.Remove(bron);
            return Json(true);
        }

    }
}
