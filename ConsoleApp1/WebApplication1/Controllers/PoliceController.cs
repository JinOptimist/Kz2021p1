using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model;
using WebApplication1.Services;
using WebApplication1.Services.Police;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class PoliceController : Controller
    {
        // TODO: Approve police academy by Chieff
        // TODO: Implement with async methods
        // TODO: Add some checks for methods

        private readonly IPoliceService _policeService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public PoliceController(IPoliceService policeService, IMapper mapper, UserService userService)
        {
            _policeService = policeService;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Get main page about police
        /// </summary>
        /// <returns></returns>
        [HttpGet("/police/main")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get police duties.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/police/duties")]
        public IActionResult PoliceDuties()
        {
            return View();
        }

        /// <summary>
        /// Get police news.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/police/news")]
        public IActionResult PoliceNews()
        {
            return View();
        }

        /// <summary>
        /// Get all users for police.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/police/users")]
        [IsPolicmen]
        public IActionResult UsersList()
        {
            List<Citizen> users = _policeService.GetAll();

            return View(users);
        }

        [HttpGet("/police/personal-cabinet/{post}")]
        public IActionResult PersonalCabinet(string post)
        {
            ViewBag.Post = post;

            return View();
        }

        [HttpPost("/police/gotojail/{id}")]
        public IActionResult GoToTheJail(long id)
        {
            if (_policeService.CitizenToJail(id))
                return Ok();

            return BadRequest("Cannot delete this citizen");
        }

        [HttpPost("/police/requestPoliceAcademy")]
        public IActionResult RequestForPoliceAcademy([FromForm] PoliceAcademyRequestVM request)
        {
            _policeService.AddedToPoliceAcademy(_mapper.Map<PoliceAcademy>(request));

            return Ok();
        }

        [HttpPost("/police/addSeverity")]
        public IActionResult AddViolationForUser([FromForm] ViolationViewModel violation)
        {
            _policeService.AddViolationForUser(_mapper.Map<Violations>(violation));

            return Ok();
        }

        [HttpDelete("/police/amnestyAllSeverity/{userId}")]
        public IActionResult AmnestyAllSeverity(long userId)
        {
            _policeService.AmnestyAllSeverity(userId);

            return Ok();
        }

        [HttpPost("/police/callThePolice")]
        public IActionResult CallThePolice([FromForm] PoliceCallViewModel policeCall)
        {
            _policeService.CreateCall(_mapper.Map<PoliceCallHistory>(policeCall));

            return Ok();
        }
    }
}
