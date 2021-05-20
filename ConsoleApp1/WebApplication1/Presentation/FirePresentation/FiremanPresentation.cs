using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Services;

namespace WebApplication1.Presentation.FirePresentation
{
    public class FiremanPresentation: IFiremanPresentation
    {
        private IFiremanRepository _firemanRepository { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private IFiremanTeamRepository _firemanTeamRepository { get; set; }
        private IUserService _userService;
        private IMapper _mapper { get; set; }
        private IFireIncidentRepository _fireIncidentRepository { get; set; }

        public FiremanPresentation(IFiremanRepository workerRepository, IMapper mapper, ICitizenRepository citizenRepository, IFiremanTeamRepository firemanTeamRepository, IUserService userService, IFireIncidentRepository fireIncidentRepository)
        {
            _firemanRepository = workerRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _userService = userService;
            _fireIncidentRepository = fireIncidentRepository;

		}
		public List<FiremanViewModel> GetAllFiremen()
		{
			var viewModels = _firemanRepository
				.GetAll()
				.Select(x => _mapper.Map<FiremanViewModel>(x)).ToList();
			return viewModels;
		}
		public void CreateFireman(FiremanViewModel model)
		{
			var citizen = _citizenRepository.GetByName(model.Name);

			var m = _mapper.Map<Fireman>(model);
			m.Citizen = citizen;
			m.CitizenId = citizen.Id;

			var fireteam = _firemanTeamRepository.GetByName(model.TeamName);
			m.FiremanTeam = fireteam;
			_firemanRepository.Save(m);
		}
		public bool Remove(long id)
		{
			var fireman = _firemanRepository.Get(id);
			if (fireman == null)
			{
				return false;
			}

			_firemanRepository.Remove(fireman);
			return true;
		}
		public FiremanViewModel GetFireman(long id)
		{
			var fireman = _firemanRepository.Get(id);
			var model = _mapper.Map<FiremanViewModel>(fireman);

			model.Name = fireman.Citizen.Name;
			model.TeamName = fireman.FiremanTeam?.TeamName;
			model.Age = fireman.Citizen.Age;
			return model;
		}
		public string Edit(FiremanViewModel model)
		{
			string user = "fireadmin";
			var fireman = _firemanRepository.Get(model.Id);
			if (fireman != null)
			{
				if (!_userService.IsFireAdmin())
				{
					var citizen = _citizenRepository.Get(fireman.CitizenId);
					citizen.Name = model.Name;
					citizen.Age = model.Age;
					fireman.WorkExperYears = model.WorkExperYears;
					_firemanRepository.Save(fireman);
					_citizenRepository.Save(citizen);

                    user = "notfireadmin";
                }
                else
                {
                    fireman.Role = model.Role;
                    fireman.FiremanTeam = _firemanTeamRepository.GetByName(model.TeamName);
                    _firemanRepository.Save(fireman);
                }
            }
            return user;
        }
        public FiremanViewModel MyProfile()
        {
            var user = _userService.GetUser();
            var fireman = _firemanRepository.GetByName(user.Name);
            if (fireman != null)
            {
                var viewModel = _mapper.Map<FiremanViewModel>(fireman);
                return viewModel;
            }
            return null;
        }
        public List<FireIncidentViewModel> GetCurrentIncidents()
        {
            var models = _fireIncidentRepository.GetCurrentFireIncidents();
            var viewmodels = new List<FireIncidentViewModel>();
            foreach (var model in models)
            {
                viewmodels.Add(_mapper.Map<FireIncidentViewModel>(model));
            }
            return viewmodels;
        }
    }
}