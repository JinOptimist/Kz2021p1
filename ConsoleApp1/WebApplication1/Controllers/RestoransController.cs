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

namespace WebApplication1.Controllers
{
    public class RestoransController : Controller
    {
        private  RestoransRepository _restoransRepository;
        private IMapper MapResto { get; set; }
        public RestoransController(RestoransRepository restoransRepository, IMapper mapper)
        {
            _restoransRepository = restoransRepository;
            MapResto = mapper;
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult AvailableResto()
        {
            var viewModels = _restoransRepository.GetAll()
                .Select(x => MapResto.Map<RestoViewModel>(x)).Where(x => x.Access == true)
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
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CreateResto()
        {
            var model = new RestoViewModel(){ };
            return View(model);
        }
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
    }
}
