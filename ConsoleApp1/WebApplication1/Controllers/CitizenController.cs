using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CitizenController : Controller
    {
        private CitizenRepository _citizenRepository;

        public CitizenController(CitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        public IActionResult Index()
        {
            var viewModels = _citizenRepository.GetAll()
                .Select(x => new FullProfileViewModel()
                    {
                        Age = x.Age,
                        Name = x.Name,
                        RegistrationDate = x.CreatingDate
                    }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult FullProfile()
        {
            var model = new FullProfileViewModel() { 
                Age = 30,
                Job = "Строитель",
                Name = "Иванов",
                RegistrationDate = DateTime.Now.AddDays(-20)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateUser(FullProfileViewModel newUser)
        {
            newUser.RegistrationDate = DateTime.Now;

            var citizen = new Citizen() { 
                Name = newUser.Name,
                Age = newUser.Age,
                CreatingDate = DateTime.Now
            };

            _citizenRepository.Save(citizen);

            return RedirectToAction("Index");
        }

        public JsonResult Remove(string name)
        {
            Thread.Sleep(2000);

            var citizen = _citizenRepository.GetByName(name);
            if (citizen == null)
            {
                return Json(false);
            }

            _citizenRepository.Remove(citizen);

            return Json(true);
        }
    }
}
