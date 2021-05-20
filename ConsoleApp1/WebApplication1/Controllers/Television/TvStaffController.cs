using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using WebApplication1.Services;

namespace WebApplication1.Controllers.Television
{
    public class TvStaffController : Controller
    {
        public TvStaffPresentation _staffPresentation { get; set; }
        private TvStaffRepository _staffRepository { get; set; }
        private TvProgrammeStaffRepository _programmeStaffRepository { get; set; }
        private IMapper _mapper { get; set; }
        private IUserService _userService { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private TvProgrammeRepository _programmeRepository { get; set; }
        public TvStaffController(TvStaffRepository staffRepository, IMapper mapper,
            IUserService userService, TvProgrammeStaffRepository programmeStaffRepository, ICitizenRepository citizenRepository,
            TvProgrammeRepository programmeRepository, TvStaffPresentation staffPresentation)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _userService = userService;
            _programmeStaffRepository = programmeStaffRepository;
            _citizenRepository = citizenRepository;
            _programmeRepository = programmeRepository;
            _staffPresentation = staffPresentation;
        }

        [IsTvDirector]
        public IActionResult Index(string channelName)
        {
            var staff = _staffPresentation.GetIndexViewModelByChannel(channelName);
            return View(staff);
        }

        [IsTvStaff]
        public IActionResult Profile()
        {
            var staffViewModel = _staffPresentation.GetProfileViewModel();
            return View(staffViewModel);
        }

        [IsTvDirector]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Citizens = _staffPresentation.AreNotTvStaff();
            return View();
        }

        [IsTvDirector]
        [HttpPost]
        public IActionResult Add(TvStaffViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Citizens = _staffPresentation.AreNotTvStaff();
                return View(viewModel);
            }

            _staffPresentation.Save(viewModel);

            ViewBag.Citizens = _staffPresentation.AreNotTvStaff();
            return View();
        }

        [IsTvDirector]
        [HttpGet]
        public IActionResult AddStaffToProgramme(long id)
        {
            ViewBag.Staff = _staffPresentation.GetStaffByChannel();

            var viewModel = _staffPresentation.PutStaffToProgramme(id);

            return View(viewModel);
        }

        [IsTvDirector]
        [HttpPost]
        public IActionResult AddStaffToProgramme(TvProgrammeStaffViewModel viewModel)
        {
            _staffPresentation.SavePutStaffToProgramme(viewModel);
            ViewBag.Staff = _staffPresentation.GetStaffByChannel();
            return View();
        }

        [IsTvDirector]
        public IActionResult StaffListOfProgramme(string programmeName)
        {
            var viewModels = _staffPresentation.GetStaffByProgramme(programmeName);
            return PartialView("_ListOfProgramme", viewModels);
        }

        public IActionResult ProgrammeListOfStaff(string name)
        {
            var programmes = _staffPresentation.GetProgrammeByStaff(name);
            return View(programmes);
        }
    }
}
