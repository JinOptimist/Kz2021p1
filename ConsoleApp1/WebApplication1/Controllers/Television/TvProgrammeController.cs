using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class TvProgrammeController : Controller
    {
        private TvProgrammePresentation _programmePresentation { get; set; }

        public TvProgrammeController(TvProgrammePresentation programmePresentation)
        {
            _programmePresentation = programmePresentation;
        }

        public IActionResult Index()
        {
            var programmes = _programmePresentation.GetIndexViewModel();
            return View(programmes);
        }

        public IActionResult IndexByChannel(string channelName)
        {
            var programmes = _programmePresentation.GetListViewModel(channelName);
            ViewBag.ChannelName = channelName;
            return View(programmes);
        }

        [IsTvDirector]
        public IActionResult List(string channelName)
        {
            var programmes = _programmePresentation.GetListViewModel(channelName);
            return View(programmes);
        }

        public IActionResult Profile(string programmeName)
        {
            var viewModel = _programmePresentation.GetProfileViewModel(programmeName);
            return View(viewModel);
        }


        [IsTvDirector]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [IsTvDirector]
        [HttpPost]
        public async Task<IActionResult> Add(TvProgrammeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_programmePresentation.NameExist(viewModel.Name))
            {
                ViewBag.ErrorMsg = "This name already exists";
                return View(viewModel);
            }
            else
            {
                await _programmePresentation.SaveModel(viewModel);
                return View(viewModel);
            }
        }

        [IsTvDirector]
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var viewModel = _programmePresentation.FindViewModel(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [IsTvDirector]
        [HttpPost]
        public async Task<IActionResult> Edit(TvProgrammeShortViewModel viewModel)
        {
            if (viewModel.AvatarFile != null)
            {
                viewModel.AvatarUrl = await _programmePresentation.UploadAvatar(viewModel.AvatarFile);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (_programmePresentation.NameExistForEdit(viewModel.Name, viewModel.Id))
            {
                ViewBag.ErrorMsg = "This name already exists";
                return View(viewModel);
            }
            else
            {
                _programmePresentation.Edit(viewModel);
                return View(viewModel);
            }

        }

        [IsTvDirector]
        public JsonResult Delete(long id)
        {
            return Json(_programmePresentation.Delete(id));
        }
    }
}
