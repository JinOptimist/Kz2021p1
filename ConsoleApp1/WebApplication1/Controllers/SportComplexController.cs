using Microsoft.AspNetCore.Http;
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
    public class SportComplexController : Controller
    {
        private SportComplexRepository _SportComplexRepository; 
        public SportComplexController(SportComplexRepository sportComplexRepository)
        {
            _SportComplexRepository = sportComplexRepository;
        }
        public ActionResult Index()
        {
            var viewModels = _SportComplexRepository.GetAll()
                .Select(x => new SportComplexViewModel()
                {
                    Name = x.Name
                }).ToList();
            return View(viewModels);
    
        }

        [HttpGet]
        public IActionResult AddComplex()
        {
            /*
            var model = new SportComplexViewModel()
            {
                Name = "SPORT",
            };
            
            return View(model);
            */
            return View();
        }
        [HttpPost]
        public ActionResult CreateComplex(SportComplexViewModel newComplex)
        {
            var complex = new SportComplex()
            {
                Name = newComplex.Name,
                CountOfEmployees = newComplex.CountOfEmployees
            };

            _SportComplexRepository.Save(complex);

            return RedirectToAction("Index");
        }
 
    }
}
