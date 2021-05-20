using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;
using WebApplication1.Services;
using WebApplication1.Utils.Police;
using WebApplication1.ViewModels;

namespace WebApplication1.Presentation.Police
{
	public class PolicePresentation : IPolicePresentation
	{
		private const int COUNT_OF_QUESTION = 5; 

		private readonly IPoliceRepository _policeRepo;
		private readonly ICitizenRepository _citizenRepo;
		private readonly IPoliceAcademyRepository _policeAcademyRepo;
		private readonly IUserService _userService;
		private readonly IViolationsRepository _violationsRepository;
		private readonly IPoliceCallRepository _policeCallRepo;
		private readonly IBlobService _blobService;
		private readonly IMapper _mapper;
		private readonly IQuestionsRepository _questionsRepo;
		private readonly IAnswerRepository _answerRepo;
		private readonly IShiftRepository _shiftRepo;

		public PolicePresentation(
			IPoliceRepository policeRepo,
			ICitizenRepository citizenRepo,
			IPoliceAcademyRepository policeAcademyRepo,
			IUserService userService,
			IViolationsRepository violationsRepository,
			IPoliceCallRepository policeCallRepo,
			IQuestionsRepository questionsRepo,
			IAnswerRepository answerRepo,
			IShiftRepository shiftRepo,
			IMapper mapper,
			IBlobService blobService
			)
		{
			_policeRepo = policeRepo;
			_citizenRepo = citizenRepo;
			_policeAcademyRepo = policeAcademyRepo;
			_userService = userService;
			_violationsRepository = violationsRepository;
			_policeCallRepo = policeCallRepo;
			_questionsRepo = questionsRepo;
			_answerRepo = answerRepo;
			_mapper = mapper;
			_blobService = blobService;
			_shiftRepo = shiftRepo;
		}

		public List<Citizen> GetAll()
		{
			List<Citizen> users = _citizenRepo.GetAll();

			return users;
		}

		public bool CitizenToJail(long id)
		{
			Citizen citizen = _citizenRepo.Get(id);

			if (citizen == null)
				return false;

			_citizenRepo.Remove(citizen);

			return true;
		}

		public bool AddedToPoliceAcademy(PoliceAcademy policeAcademy)
		{
			Citizen citizen = _userService.GetUser();

			if (citizen is null) return false;

			policeAcademy.Citizen = citizen;

			_policeAcademyRepo.Save(policeAcademy);

			return true;
		}

		public long AddViolationForUser(Violations violation)
		{
			Citizen user = _userService.GetUser();

			violation.PolicemanId = user.Policeman.Id;

			_violationsRepository.Save(violation);

			return violation.Id;
		}

		public bool AmnestySeverity(long id)
		{
			Violations citizenViolations = _violationsRepository.Get(id);

			_violationsRepository.Remove(citizenViolations);

			return true;
		}

		public bool CreateCall(PoliceCallHistory policeCallHistory)
		{
			policeCallHistory.DateCall = DateTime.Now;

			List<long> allPolicmanId = _policeRepo.GetAllAsIQueryable()
												  .Select(p => p.Id)
												  .ToList();

			long randomPolicemanId = PolicemanUtils.SearchRandom(allPolicmanId);

			policeCallHistory.PolicemanId = randomPolicemanId;

			Citizen citizen = _userService.GetUser();

			policeCallHistory.Citizen = citizen;

			_policeCallRepo.Save(policeCallHistory);

			return true;
		}

		public List<UserViolationViewModel> GetAllUserViolations(long id)
		{
			List<Violations> userViolations = _violationsRepository.GetAllAsIQueryable()
																   .Where(v => v.Citizen.Id == id)
																   .ToList();

			return _mapper.Map<List<UserViolationViewModel>>(userViolations); ;
		}

		public async Task<UserInfoViewModel> GetUserInfo(long id)
		{
			Citizen user = _citizenRepo.GetAllAsIQueryable()
									   .FirstOrDefault(c => c.Id == id);

			UserInfoViewModel userInfo = _mapper.Map<UserInfoViewModel>(user);

			Uri uri = await _blobService.GetPhotoAsync(id.ToString());

			userInfo.Uri = (await _blobService.GetPhotoAsync(id.ToString())).AbsoluteUri;

			return userInfo;
		}

		public List<PolicemanViewModel> GetAllPolicemen()
		{
			List<Policeman> policemen = _policeRepo.GetAllAsIQueryable()
												   .Take(10)
												   .ToList();

			return _mapper.Map<List<PolicemanViewModel>>(policemen);
		}

		public void DismissPoliceman(long id)
		{
			Policeman policman = _policeRepo.GetAllAsIQueryable()
											.SingleOrDefault(x => x.Id == id);

			_policeRepo.Remove(policman);
		}

		public List<PoliceApplicantViewModel> GetAllApplicants()
		{
			List<PoliceAcademy> requests = _policeAcademyRepo.GetAllAsIQueryable()
															 .Where(pc => pc.RequestStatus == RequestStatus.InProcess)
															 .ToList();

			return _mapper.Map<List<PoliceApplicantViewModel>>(requests);
		}

		public void AcceptApplicant(long id)
		{
			PoliceAcademy applicant = _policeAcademyRepo.GetAllAsIQueryable()
														.SingleOrDefault(pc => pc.Id == id);

			_policeAcademyRepo.Remove(applicant);

			Policeman policeman = new Policeman {
				CitizenId = applicant.CitizenId,
				StartWork = DateTime.Now
			};

			_policeRepo.Save(policeman);
		}

		public long AddNewQuestion(PoliceQuizQuestion question)
		{
			_questionsRepo.Save(question);

			return question.Id;
		}

		public void AddAnswers(List<PoliceQuizAnswer> answers)
		{
			answers.ForEach(a => _answerRepo.Save(a));
		}

		public List<QuestionAndAnswer> GetQuiz()
		{
			IQueryable<PoliceQuizQuestion> allRecords = _questionsRepo.GetAllAsIQueryable();

			int randomNumber = new Random().Next(1, allRecords.Count());

			List<PoliceQuizQuestion> listQestion = allRecords
											.Skip(randomNumber)
											.Take(COUNT_OF_QUESTION)
											.ToList();

			return _mapper.Map<List<QuestionAndAnswer>>(listQestion);
		}

		public bool? CheckAnswer(long id)
		{
			return _answerRepo.GetAllAsIQueryable()
							  .FirstOrDefault(a => a.Id == id)?.IsRight;
		}

		public void UpRank(Rank rank)
		{
			Citizen user = _userService.GetUser();

			Policeman policeman = _policeRepo.GetAllAsIQueryable()
											 .SingleOrDefault(x => x.CitizenId == user.Id);

			policeman.Rank = rank;

			_policeRepo.Save(policeman);
		}

		public void SetPoliceShift(PoliceShift shift)
		{
			_shiftRepo.Save(shift);
		}

		public IEnumerable<BasePolicemanShiftVM> GetShifts()
		{
			Citizen user = _userService.GetUser();

			List<PoliceShift> shifts = _shiftRepo.GetAllAsIQueryable()
										   .Where(x => x.PolicemanId == user.Policeman.Id)
										   .ToList();

			return _mapper.Map<List<BasePolicemanShiftVM>>(shifts);
		}

		public IEnumerable<SheriffShiftVM> GetOfficerShift()
		{
			List<PoliceShift> shifts = _shiftRepo.GetAllAsIQueryable()
										   .Take(10)
										   .ToList();

			return _mapper.Map<List<SheriffShiftVM>>(shifts);
		}

		public PoliceShift UpdateShift(UpdateShiftViewModel shift)
		{
			PoliceShift oldshift = _shiftRepo.GetAllAsIQueryable()
											 .FirstOrDefault(x => x.Id == shift.Id);

			oldshift.StartDate = shift.StartDate;
			oldshift.EndDate = shift.EndDate;

			_shiftRepo.Save(oldshift);

			return oldshift;
		}
	}
}
