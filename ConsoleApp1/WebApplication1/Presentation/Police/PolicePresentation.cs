using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;
using WebApplication1.Services;
using WebApplication1.Utils.Police;
using WebApplication1.ViewModels;

namespace WebApplication1.Presentation.Police
{
	public class PolicePresentation : IPolicePresentation
	{
		// TODO: Implement with contracts
		private readonly IPoliceRepository _policeRepo;
		private readonly ICitizenRepository _citizenRepo;
		private readonly IPoliceAcademyRepository _policeAcademyRepo;
		private readonly IUserService _userService;
		private readonly IViolationsRepository _violationsRepository;
		private readonly IPoliceCallRepo _policeCallRepo;
		//private readonly IBlobService _blobService;
		private readonly IMapper _mapper;
		private readonly IQuestionsRepo _questionsRepo;
		private readonly IAnswerRepo _answerRepo;
		private readonly IShiftRepo _shiftRepo;

		public PolicePresentation(
			IPoliceRepository policeRepo,
			ICitizenRepository citizenRepo,
			IPoliceAcademyRepository policeAcademyRepo,
			IUserService userService,
			IViolationsRepository violationsRepository,
			IPoliceCallRepo policeCallRepo,
			IQuestionsRepo questionsRepo,
			IAnswerRepo answerRepo,
			IShiftRepo shiftRepo,
			IMapper mapper//,
			//IBlobService blobService
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
			//_blobService = blobService;
			_shiftRepo = shiftRepo;
		}

		public List<Citizen> GetAll()
		{
			List<Citizen> users = _citizenRepo.GetAll().ToList();

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

			policeAcademy.CitizenId = citizen.Id;

			_policeAcademyRepo.Save(policeAcademy);

			return true;
		}

		public long AddViolationForUser(Violations violation)
		{
			long? userId = _userService.GetUser()?.Id;

			long policemanId = _policeRepo.GetAll().SingleOrDefault(p => p.CitizenId == userId).Id;

			violation.PolicemanId = policemanId;

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

			int countPolicemen = _policeRepo.GetAll().Count();

			long randomPolicemanId = PolicemanUtils.SearchRandom(countPolicemen);

			policeCallHistory.PolicemanId = randomPolicemanId;

			long userId = _userService.GetUser().Id;

			policeCallHistory.CitizenId = userId;

			_policeCallRepo.Save(policeCallHistory);

			return true;
		}

		public List<UserViolationViewModel> GetAllUserViolations(long id)
		{
			List<Violations> userViolations = _violationsRepository.GetAllAsIQueryable()
																   .Where(v => v.CitizenId == id)
																   .ToList();

			return _mapper.Map<List<UserViolationViewModel>>(userViolations); ;
		}

		/*public async Task<UserInfoViewModel> GetUserInfo(long id)
		{
			Citizen user = _citizenRepo.GetAllAsIQueryable()
									   .Include(p => p.House)
									   .FirstOrDefault(c => c.Id == id);

			UserInfoViewModel userInfo = _mapper.Map<UserInfoViewModel>(user);

			Uri uri = await _blobService.GetPhotoAsync(id.ToString());

			userInfo.Uri = (await _blobService.GetPhotoAsync(id.ToString())).AbsoluteUri;

			return userInfo;
		}*/

		public List<PolicemanViewModel> GetAllPolicemen()
		{
			List<Policeman> policemen = _policeRepo.GetAllAsIQueryable()
												  .Include(x => x.Citizen)
												  .Take(10)
												  .ToList();

			return _mapper.Map<List<PolicemanViewModel>>(policemen);
		}

		public void DismissPoliceman(long id)
		{
			Policeman policman = _policeRepo.GetAllAsIQueryable()
											.Include(p => p.PoliceCallHistories)
											.Include(p => p.Violations)
											.SingleOrDefault(x => x.Id == id);

			_policeRepo.Remove(policman);
		}

		public List<ApplicantViewModel> GetAllApplicants()
		{
			List<PoliceAcademy> requests = _policeAcademyRepo.GetAllAsIQueryable()
															 .Where(pc => pc.RequestStatus == RequestStatus.InProcess)
															 .ToList();

			return _mapper.Map<List<ApplicantViewModel>>(requests);
		}

		public void AcceptApplicant(long id)
		{
			PoliceAcademy applicant = _policeAcademyRepo.GetAllAsIQueryable()
														.SingleOrDefault(pc => pc.Id == id);

			_policeAcademyRepo.Remove(applicant);

			Policeman policeman = new Policeman
				{ CitizenId = applicant.CitizenId };

			_policeRepo.Save(policeman);
		}

		public void AddNewQuestionsAndAnswers(List<QuestionAndAnswer> questionAndAnswers)
		{
			List<Question> questions = _mapper.Map<List<Question>>(questionAndAnswers);

			questions.ForEach(question =>
			{
				_questionsRepo.Save(question);
			});

			List<Answer> answersWithId = _mapper.Map<List<Answer>>(questions);

			List<Answer> ss = _mapper.Map(questionAndAnswers, answersWithId);
		}

		public long AddNewQuestion(Question question)
		{
			_questionsRepo.Save(question);

			return question.Id;
		}

		public void AddAnswers(List<Answer> answers)
		{
			answers.ForEach(a => _answerRepo.Save(a));
		}

		public List<QuestionAndAnswer> GetQuiz()
		{
			IQueryable<Question> allRecords = _questionsRepo.GetAllAsIQueryable();

			int randomNumber = new Random().Next(1, allRecords.Count());

			List<Question> listQestion = allRecords
											.Skip(randomNumber)
											.Take(5)
											.ToList();

			return _mapper.Map<List<QuestionAndAnswer>>(listQestion);
		}

		public bool? CheckAnswer(long id)
		{
			return _answerRepo.GetAllAsIQueryable().FirstOrDefault(a => a.Id == id)?.IsRight;
		}

		public void UpRank(Rank rank)
		{
			Citizen user = _userService.GetUser();

			Policeman policeman = _policeRepo.GetAllAsIQueryable()
											 .SingleOrDefault(x => x.CitizenId == user.Id);

			policeman.Rank = rank;

			_policeRepo.Save(policeman);
		}

		public void SetPoliceShift(Shift shift)
		{
			_shiftRepo.Save(shift);
		}

		public IEnumerable<BasePolicemanShiftVM> GetShifts()
		{
			Citizen user = _userService.GetUser();

			List<Shift> shifts = _shiftRepo.GetAllAsIQueryable()
										   .Where(x => x.PolicemanId == user.Policeman.Id)
										   .ToList();

			return _mapper.Map<List<BasePolicemanShiftVM>>(shifts);
		}

		public IEnumerable<SheriffShiftVM> GetOfficerShift()
		{
			List<Shift> shifts = _shiftRepo.GetAllAsIQueryable()
										   .Take(10)
										   .ToList();

			return _mapper.Map<List<SheriffShiftVM>>(shifts);
		}

		public Shift UpdateShift(UpdateShiftViewModel shift)
		{
			Shift oldshift = _shiftRepo.GetAllAsIQueryable()
									   .FirstOrDefault(x => x.Id == shift.Id);

			oldshift.StartDate = shift.StartDate;
			oldshift.EndDate = shift.EndDate;

			_shiftRepo.Save(oldshift);

			return oldshift;
		}
	}
}
