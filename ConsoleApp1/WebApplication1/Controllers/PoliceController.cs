using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers.CustomFilterAttributes;
using WebApplication1.EfStuff.Model;
using WebApplication1.Presentation.Police;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("{controller}")]
	public class PoliceController : Controller
	{
		private readonly IPolicePresentation _policePresentation;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IBlobService _blobService;

		public PoliceController(IPolicePresentation policePresentation, IMapper mapper, IUserService userService, IBlobService blobService)
		{
			_policePresentation = policePresentation;
			_mapper = mapper;
			_userService = userService;
			_blobService = blobService;
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
		/// Enter in your cabinet for manage your oppartunity
		/// The possibilities depend on your role
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/police-personal-cabinet")]
		
		public IActionResult PolicePersonalCabinet()
		{
			Citizen user = _userService.GetUser();

			return View(user);
		}

		/// <summary>
		/// Get full profile about selected user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("/police/user-info/{id}")]
		[IsActiveDuty]
		public async Task<IActionResult> UserInfo(long id)
		{
			UserInfoViewModel user = await _policePresentation.GetUserInfo(id);
			List<UserViolationViewModel> userViolations = _policePresentation.GetAllUserViolations(id);

			UserFullInformationViewModel fullInfo = new UserFullInformationViewModel
			{
				UserInfo = user,
				UserViolations = userViolations
			};

			return View(fullInfo);
		}

		/// <summary>
		/// Get all users
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/users-list")]
		[IsActiveDuty]
		public PartialViewResult UserList()
		{
			return PartialView("Views/Shared/Police/UsersList.cshtml", _policePresentation.GetAll());
		}

		/// <summary>
		/// Delete citizen
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost("/police/gotojail/{id}")]
		[IsActiveDuty]
		public IActionResult GoToTheJail(long id)
		{
			if (_policePresentation.CitizenToJail(id))
				return RedirectToAction("PolicePersonalCabinet");

			return BadRequest("Cannot delete this citizen");
		}

		/// <summary>
		/// Create application form for citizen
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("/police/requestPoliceAcademy")]
		public IActionResult RequestForPoliceAcademy([FromForm] PoliceAcademyRequestVM request)
		{
			PoliceAcademy policeAcademy = _mapper.Map<PoliceAcademy>(request);

			policeAcademy.RequestStatus = RequestStatus.InProcess;

			_policePresentation.AddedToPoliceAcademy(policeAcademy);

			return Ok();
		}

		/// <summary>
		/// Added severity for selected citizen
		/// </summary>
		/// <param name="violation"></param>
		/// <returns></returns>
		[HttpPost("/police/addSeverity")]
		[IsActiveDuty]
		public IActionResult AddViolationForUser([FromForm] ViolationViewModel violation)
		{
			long violationId = _policePresentation.AddViolationForUser(_mapper.Map<Violations>(violation));

			return Ok(violationId);
		}

		/// <summary>
		/// Remove selected severity
		/// </summary>
		/// <param name="severityId"></param>
		/// <returns></returns>
		[HttpDelete("/police/amnestySeverity/{severityId}")]
		[IsActiveDuty]
		public IActionResult AmnestyAllSeverity(long severityId)
		{
			_policePresentation.AmnestySeverity(severityId);

			return Ok();
		}

		/// <summary>
		/// Create call for citizen
		/// </summary>
		/// <param name="policeCall"></param>
		/// <returns></returns>
		[HttpPost("/police/callThePolice")]
		public IActionResult CallThePolice([FromForm] PoliceCallViewModel policeCall)
		{
			_policePresentation.CreateCall(_mapper.Map<PoliceCallHistory>(policeCall));

			return Ok();
		}

		/// <summary>
		/// Upload userphoto for policman
		/// </summary>
		/// <param name="fileUpload"></param>
		/// <returns></returns>
		[HttpPost("/police/uploadUserPhoto")]
		public async Task<IActionResult> UploadUserPhoto([FromForm] FileUpload fileUpload)
		{
			Uri uri = await _blobService.UploadPhotoAsync(fileUpload);

			return Ok(uri.AbsoluteUri);
		}
		
		/// <summary>
		/// Get all workers.
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/policemen-list")]
		[IsSheriff]
		public IActionResult GetPolicemenList()
		{
			return PartialView("Views/Shared/Police/PolicemenList.cshtml", _policePresentation.GetAllPolicemen());
		}

		/// <summary>
		/// Dismiss policman and delete all infromation about him
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("/police/dismiss-policeman/{id}")]
		[IsSheriff]
		public IActionResult DismissPoliceman(long id)
		{
			_policePresentation.DismissPoliceman(id);

			return Ok();
		}

		/// <summary>
		/// Get all applicants. For manage them. 
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/get-applicants")]
		[IsSheriff]
		public IActionResult GetAllApplicants()
		{
			return PartialView("Views/Shared/Police/ApplicantsList.cshtml", _policePresentation.GetAllApplicants());
		}

		/// <summary>
		/// Accept appilcant for policeacademy.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPatch("/police/accept-applicant/{id}")]
		[IsSheriff]
		public IActionResult AcceptApplicant(long id)
		{
			_policePresentation.AcceptApplicant(id);

			return Ok();
		}

		/// <summary>
		/// Get quizer for trainee. Quizer is test. 
		/// Trainee must pass this for 100%. 
		/// And become officer.
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/get-quizer")]
		
		public IActionResult GetQuizerPage()
		{
			return PartialView("Views/Shared/Police/Quizer.cshtml");
		}

		/// <summary>
		/// Manage question for quize. Add quiz question.
		/// </summary>
		/// <param name="questions"></param>
		/// <returns></returns>
		[HttpPost("/police/add-new-question")]
		[IsSheriff]
		public IActionResult AddNewQuestion([FromForm] PoliceQuizQuestion questions)
		{
			long id = _policePresentation.AddNewQuestion(questions);

			return Ok(id);
		}

		/// <summary>
		/// New answers for selected question.
		/// </summary>
		/// <param name="answers"></param>
		/// <returns></returns>
		[HttpPost("/police/add-new-answers")]
		[IsSheriff]
		public JsonResult AddNewAnswer(List<PoliceQuizAnswer> answers)
		{
			_policePresentation.AddAnswers(answers);

			return Json("");
		}

		/// <summary>
		/// Get questions and answers for quiz
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/get-quiz")]
		[IsTrainee]
		public JsonResult GetQestionAndAnswers()
		{
			return Json(_policePresentation.GetQuiz());
		}

		/// <summary>
		/// Check is correct answer
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("/police/check-answer/{id}")]
		[IsTrainee]
		public IActionResult CheckAnswer(long id)
		{
			bool? isRightAnswer = _policePresentation.CheckAnswer(id);

			return Ok(isRightAnswer);
		}

		/// <summary>
		/// If quiz equal 100%. Trainee become officer.
		/// </summary>
		/// <returns></returns>
		[HttpPatch("/police/end-quiz")]
		[IsTrainee]
		public IActionResult EndQuiz()
		{
			_policePresentation.UpRank(Rank.Officer);
			return Ok();
		}

		/// <summary>
		/// Set shift for officer
		/// </summary>
		/// <param name="shiftViewModel"></param>
		/// <returns></returns>
		[HttpPost("/police/set-shift")]
		[IsSheriff]
		public IActionResult SetShift(BasePolicemanShiftVM shiftViewModel)
		{
			PoliceShift shift = _mapper.Map<PoliceShift>(shiftViewModel);

			_policePresentation.SetPoliceShift(shift);

			return Json(new BasePolicemanShiftVM());
		}

		/// <summary>
		/// View your shifts
		/// </summary>
		/// <returns></returns>
		[HttpGet("/police/get-shift")]
		[IsActiveDuty]
		public IActionResult GetShift()
		{
			if (_userService.IsOfficer())
			{
				IEnumerable<BasePolicemanShiftVM> shifts = _policePresentation.GetShifts();
				return PartialView("Views/Shared/Police/ShiftList.cshtml", shifts);
			}

			if (_userService.IsSheriff())
			{
				IEnumerable<SheriffShiftVM> shifts = _policePresentation.GetOfficerShift();
				return PartialView("Views/Shared/Police/OfficerShiftsList.cshtml", shifts);
			}

			return NotFound();
		}

		/// <summary>
		/// Update created shift.
		/// </summary>
		/// <param name="shift"></param>
		/// <returns></returns>
		[HttpPatch("/police/update-shift")]
		[IsSheriff]
		public IActionResult UpdateShift(UpdateShiftViewModel shift)
		{
			BasePolicemanShiftVM newshift = 
				_mapper.Map<BasePolicemanShiftVM>(_policePresentation.UpdateShift(shift));

			return Json(newshift);
		}
	}
}
