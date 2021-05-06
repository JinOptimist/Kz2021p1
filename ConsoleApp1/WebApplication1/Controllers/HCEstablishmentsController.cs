using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HCEstablishmentsController : Controller
    {
        private HCEstablishmentsRepository HCEstablishmentsRepository;
        private IMapper Mapper;
        private UserService _userService;
        public HCEstablishmentsController(HCEstablishmentsRepository establishmentsRepository, IMapper mapper)
        {
            HCEstablishmentsRepository = establishmentsRepository;
            Mapper = mapper;
        }
        public IActionResult Index()
        {
            var user = _userService.GetUser();
            var viewmodel = Mapper.Map<HCEstablishments>(user);

            return View(viewmodel);
        }
        [HttpGet]
        public IActionResult CreateEstablishment()
        {
            var model = new HCEstablishmentsViewModel()
            {
                Name = "Mediker",
                Address = "st.Wiliam",
                Contacts = 212151,
                Webpage = "rrr.com"
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEstablishment(HCEstablishmentsViewModel newEstablishment)
        {
            var model = Mapper.Map<HCEstablishments>(newEstablishment);

            HCEstablishmentsRepository.Save(model);

            return RedirectToAction("Index");
        }

        public JsonResult Remove(long id)
        {
            var userForDelete = HCEstablishmentsRepository.Get(id);

            if(userForDelete == null)
            {
                return Json(false);
            }

            HCEstablishmentsRepository.Remove(userForDelete);

            return Json(true);
        }
    }
}
