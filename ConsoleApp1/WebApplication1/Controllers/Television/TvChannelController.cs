using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models;
using WebApplication1.Models.Television;
using WebApplication1.Presentation.Television;

namespace WebApplication1.Controllers.Television
{
    public class TvChannelController : Controller
    {
        private TvChannelPresentation _channelPresentation { get; set; }
        public TvChannelController(TvChannelPresentation channelPresentation)
        {
            _channelPresentation = channelPresentation;
        }

        public IActionResult Index()
        {
            var channels = _channelPresentation.GetIndexViewModel();
            return View(channels);
        }

        public IActionResult Profile(string channelName)
        {
            var viewModel = _channelPresentation.ProfileViewModel(channelName);
            return View(viewModel);
        }

        [IsTvAdmin]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [IsTvAdmin]
        [HttpPost]
        public IActionResult Add(TvChannelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_channelPresentation.NameExist(viewModel.Name))
            {
                ViewBag.ErrorMsg = "This name already exists";
                return View(viewModel);
            }
            else
            {
                _channelPresentation.Save(viewModel);
                return RedirectToAction("Index");
            }
        }

        [IsTvAdmin]
        [HttpGet]
        public IActionResult AddDirector(string channelName)
        {
            var viewModel = _channelPresentation.NewDirectorViewModel(channelName);
            ViewBag.Citizens = _channelPresentation.AreNotTvStaff();
            return View(viewModel);
        }

        [IsTvAdmin]
        [HttpPost]
        public IActionResult AddDirector(TvStaffViewModel viewModel)
        {
            _channelPresentation.SaveDirector(viewModel);
            ViewBag.Citizens = _channelPresentation.AreNotTvStaff();
            return View(viewModel);
        }
    }
}
