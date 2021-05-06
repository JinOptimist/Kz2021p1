using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Presentation;
using WebApplication1.RestoBusiness;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Localized]
    public class AdminsRestoController : Controller
    {
        private IAdminRestoRepository _adminRestoRepository;
        private RestoransRepository _restoransRepository;
        private IUserService UserService { get; set; }
        private AdminRestoService AdminRestoService { get; set; }
        public AdminsRestoController(RestoransRepository restoransRepository , IAdminRestoRepository adminRestoRepository,
            IUserService userService, AdminRestoService adminRestoService)
        {
            _restoransRepository = restoransRepository;
            _adminRestoRepository = adminRestoRepository;
            UserService = userService;
            AdminRestoService = adminRestoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginAdminViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var adminResto = _adminRestoRepository.Login(viewModel.LoginAdmin, viewModel.PasswordAdmin);
            if (adminResto == null)
            {
                ModelState.AddModelError(nameof(LoginAdminViewModel.LoginAdmin), "Не правильный логин или пароль");
                return View(viewModel);
            }
            var pages = new List<Claim>() {
                new Claim("AdminResto", adminResto.LoginAdmin.ToString()),
            };
            var claimsIdenity = AdminRestoService.GetClaimsIdentity(Startup.AuthAdminR);
            // !(claimsIdenity.AuthenticationType is null) && claimsIdenity.AuthenticationType == Startup.AuthAdminR
            if ( !(claimsIdenity is null) &&!string.IsNullOrEmpty(claimsIdenity.AuthenticationType)&& claimsIdenity.AuthenticationType == Startup.AuthAdminR)
            {
                claimsIdenity.AddClaims(pages);
            }
            else
            {
                claimsIdenity = new ClaimsIdentity(pages, Startup.AuthAdminR);
                AdminRestoService.AddAdminClaim(claimsIdenity);
            }
            await HttpContext.SignInAsync(AdminRestoService.GetClaimsPrincipal());
            return RedirectToAction("MainPage", "Restorans");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginAdminViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var resto = _restoransRepository.GetByName(viewModel.NameResto);

            var adminresto = new AdminResto()
            {
                LoginAdmin = viewModel.LoginAdmin,
                PasswordAdmin = viewModel.PasswordAdmin,
                Restoran = resto,
                Citizen = UserService.GetUser()
            };
            _adminRestoRepository.Save(adminresto);

            return RedirectToAction("MainPage", "Restorans");
        }

        public async Task<IActionResult> Exit()
        {
            AdminRestoService.RemoveAdminClaim();
            await HttpContext.SignInAsync(AdminRestoService.GetClaimsPrincipal());
            return RedirectToAction("MainPage", "Restorans");
        }
    }
}
