using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class CitizenController : Controller
    {
        private CitizenRepository _citizenRepository;
        private CitizenPresentation _citizenPresentation;

        public CitizenController(CitizenRepository citizenRepository, 
            CitizenPresentation citizenPresentation)
        {
            _citizenRepository = citizenRepository;
            _citizenPresentation = citizenPresentation;
        }

        public IActionResult Index()
        {
            var viewModels = _citizenPresentation.GetIndexViewModel();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = _citizenRepository.Login(viewModel.Name, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(LoginViewModel.Name), "Не правильный логин или пароль");
                return View(viewModel);
            }

            var claimsPrincipal = _citizenPresentation.GetClaimsPrincipal(user);
            await HttpContext.SignInAsync(claimsPrincipal);

            return View();
        }

        public async Task<IActionResult> Exit()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _citizenPresentation.Save(viewModel);
            return RedirectToAction("Index", "Home");
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
