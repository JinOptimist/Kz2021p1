using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models;
using WebApplication1.Models.Television;
using WebApplication1.Presentation.Television;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Television
{
    public class TvCelebrityController : Controller
    {
        private TvCelebrityPresentation _celebrityPresentation { get; set; }
        public TvCelebrityController(
            TvCelebrityPresentation celebrityPresentation)
        {
            _celebrityPresentation = celebrityPresentation;
        }

        public IActionResult Index()
        {
            var viewModels = _celebrityPresentation.GetIndexViewModel();
            return View(viewModels);
        }

        [IsTvCastingDirector]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Citizens = _celebrityPresentation.AreNotCelebrity();
            return View();
        }

        [IsTvCastingDirector]
        [HttpPost]
        public IActionResult Add(TvCelebrityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Citizens = _celebrityPresentation.AreNotCelebrity();
                return View(viewModel);
            }

            _celebrityPresentation.Save(viewModel);

            ViewBag.Citizens = _celebrityPresentation.AreNotCelebrity();
            return View();
        }

        [IsTvCastingDirector]
        [HttpGet]
        public IActionResult AddToProgramme(long id)
        {
            ViewBag.Celebrity = _celebrityPresentation.GetIndexViewModel();
            var viewModel = _celebrityPresentation.PutCelebrityToProgramme(id);
            return View(viewModel);
        }

        [IsTvCastingDirector]
        [HttpPost]
        public IActionResult AddToProgramme(TvProgrammeCelebrityViewModel viewModel)
        {
            _celebrityPresentation.SavePutCelebrityToProgramme(viewModel);
            ViewBag.Celebrity = _celebrityPresentation.GetIndexViewModel();
            return View();
        }

        public IActionResult CelebrityListOfProgramme(string programmeName)
        {
            var viewModels = _celebrityPresentation.GetCelebrityByProgramme(programmeName);
            return PartialView("_CelebrityListOfProgramme", viewModels);
        }
    }
}
