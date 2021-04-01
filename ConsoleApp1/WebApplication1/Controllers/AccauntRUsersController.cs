using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccauntRUsersController : Controller
    {
        private  UsersRestoRepository _usersRestoRepository;
        private RolesRestoRepository _rolesRestoRepository;
        private IMapper MapResto { get; set; }
        public AccauntRUsersController(UsersRestoRepository usersRestoRepository, RolesRestoRepository rolesRestoRepository, IMapper mapper)
        {
            _usersRestoRepository = usersRestoRepository;
            _rolesRestoRepository = rolesRestoRepository;
            MapResto = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(LoginUsersModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRestoRepository.GetAllU().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Restorans");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Register(RegisterUsersModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRestoRepository.GetAll().FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new UsersResto { Email = model.Email, Password = model.Password };
                    RolesResto userRole = _rolesRestoRepository.GetAll().FirstOrDefault(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    _usersRestoRepository.Save(user);

                    Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Restorans");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private void Authenticate(UsersResto userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userName.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public  IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "AccauntRUsers");
        }
    }
}
