using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models.Television;
using WebApplication1.Presentation.Television;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Television
{
    public class TvScheduleController : Controller
    {
        private TvSchedulePresentation _schedulePresentation { get; set; }
        public TvScheduleController(TvSchedulePresentation schedulePresentation)
        {
            _schedulePresentation = schedulePresentation;
        }

        public IActionResult Index(string channelName, string date)
        {
            ViewBag.ChannelName = channelName;
            var schedules = _schedulePresentation.GetIndexViewModel(channelName, date);
            return View(schedules);
        }

        [IsTvDirector]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
            return View();
        }

        [IsTvDirector]
        [HttpPost]
        public IActionResult Add(TvScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
                return View(viewModel);
            }

            _schedulePresentation.Save(viewModel);
            ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
            return View();
        }

        [IsTvDirector]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
            var schedule = _schedulePresentation.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [IsTvDirector]
        [HttpPost]
        public IActionResult Edit(TvScheduleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
                return View(viewModel);
            }

            _schedulePresentation.Edit(viewModel);
            ViewBag.Programmes = _schedulePresentation.GetProgrammesViewModel();
            return View();
        }

        [IsTvDirector]
        public JsonResult Delete(long id)
        {
            return Json(_schedulePresentation.Delete(id));
        }
    }
}
