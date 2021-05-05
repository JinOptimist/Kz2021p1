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

namespace WebApplication1.Controllers
{
    public class HCEstablishmentsController : Controller
    {
        private HCEstablishmentsRepository HCEstablishmentsRepository;
        private IMapper Mapper;
        public HCEstablishmentsController(HCEstablishmentsRepository establishmentsRepository, IMapper mapper)
        {
            HCEstablishmentsRepository = establishmentsRepository;
            Mapper = mapper;
        }
        public IActionResult Index()
        {
            var viewmodel = HCEstablishmentsRepository
                .GetAll()
                .Select(x => Mapper.Map<HCEstablishmentsViewModel>(x))
                .ToList();

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
            var establishments = new HCEstablishments()
            {
                Name = newEstablishment.Name,
                Address = newEstablishment.Address,
                Contacts = newEstablishment.Contacts,
                Webpage = newEstablishment.Webpage
            };

            HCEstablishmentsRepository.Save(establishments);

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
