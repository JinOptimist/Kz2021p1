using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Presentation;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HCEstablishmentsController : Controller
    {
        private IHCEstablishmentsRepository _hcestablishmentsRepository;
        private IUserService _userService;
        private IMapper _mapper;
        private HCEstablishmentsPresentation _hcestablishmentsPresentation;
        public HCEstablishmentsController(IHCEstablishmentsRepository establishmentsRepository, 
            IMapper mapper, IUserService userService,
            HCEstablishmentsPresentation hcestablishmentsPresentation)
        {
            _hcestablishmentsRepository = establishmentsRepository;
            _mapper = mapper;
            _userService = userService;
            _hcestablishmentsPresentation = hcestablishmentsPresentation;
        }
        public IActionResult Index()
        {
            var viewModels = _hcestablishmentsPresentation.GetIndexViewModel();
            return View(viewModels);
        }

        [IsHCWorker]
        [Authorize]
        [HttpGet]
        public IActionResult CreateEstablishment()
        {
            var viewmodel = new HCEstablishmentsViewModel();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult CreateEstablishment(HCEstablishmentsViewModel newEstablishment)
        {
            var model = _mapper.Map<HCEstablishments>(newEstablishment);
            _hcestablishmentsRepository.Save(model);

            return RedirectToAction("Index");
        }
        public JsonResult Remove(long id)
        {
            return Json(_hcestablishmentsPresentation.Remove(id));
        }
    }
}
