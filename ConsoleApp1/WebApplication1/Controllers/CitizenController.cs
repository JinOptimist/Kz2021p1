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

namespace WebApplication1.Controllers
{
    public class CitizenController : Controller
    {
        private CitizenRepository _citizenRepository;
        private AdressRepository _adressRepository;

        public CitizenController(CitizenRepository citizenRepository, AdressRepository adressRepository)
        {
            _citizenRepository = citizenRepository;
            _adressRepository = adressRepository;
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

            var pages = new List<Claim>() {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthMethod)
            };

            var claimsIdenity = new ClaimsIdentity(pages, Startup.AuthMethod);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdenity);
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

            var citizen = new Citizen()
            {
                Name = viewModel.Name,
                Password = viewModel.Password
            };

            _citizenRepository.Save(citizen);

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
