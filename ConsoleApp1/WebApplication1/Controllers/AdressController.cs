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
    public class AdressController : Controller
    {
        private AdressRepository AdressRepository { get; set; }
        private IMapper Mapper { get; set; }

        public AdressController(AdressRepository adressRepository, IMapper mapper)
        {
            AdressRepository = adressRepository;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            var models = AdressRepository
                .GetAll()
                .Select(x => Mapper.Map<AdressViewModel>(x))
                .ToList();
            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AdressViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AdressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = Mapper.Map<Adress>(viewModel);

            AdressRepository.Save(model);

            return RedirectToAction("Index", "Adress");
        }
    }
}
