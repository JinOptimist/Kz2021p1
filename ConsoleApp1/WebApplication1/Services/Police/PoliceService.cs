using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Utils.Police;

namespace WebApplication1.Services.Police
{
	public class PoliceService : IPoliceService
	{
		// TODO: Implement with contracts
		private readonly PoliceRepository _policeRepo;
		private readonly CitizenRepository _citizenRepo;
		private readonly PoliceAcademyRepository _policeAcademyRepo;
		private readonly UserService _userService;
		private readonly ViolationsRepository _violationsRepository;
		private readonly PoliceCallRepo _policeCallRepo;

		public PoliceService(
			PoliceRepository policeRepo,
			CitizenRepository citizenRepo,
			PoliceAcademyRepository policeAcademyRepo,
			UserService userService,
			ViolationsRepository violationsRepository,
			PoliceCallRepo policeCallRepo
			)
		{
			_policeRepo = policeRepo;
			_citizenRepo = citizenRepo;
			_policeAcademyRepo = policeAcademyRepo;
			_userService = userService;
			_violationsRepository = violationsRepository;
			_policeCallRepo = policeCallRepo;
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

		public bool AddViolationForUser(Violations violation)
		{
			long? userId = _userService.GetUser()?.Id;

			long policemanId = _policeRepo.GetAll().SingleOrDefault(p => p.CitizenId == userId).Id;

			violation.PolicemanId = policemanId;

			_violationsRepository.Save(violation);

			return true;
		}

		public bool AmnestyAllSeverity(long id)
		{
			List<Violations> citizenViolations = _violationsRepository.GetAll().Where(v => v.CitizenId == id).ToList();

			citizenViolations.ForEach(v => _violationsRepository.Remove(v));

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
	}
}
