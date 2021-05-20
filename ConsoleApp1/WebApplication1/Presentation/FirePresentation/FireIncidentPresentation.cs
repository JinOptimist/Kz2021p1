using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Presentation.FirePresentation
{
    public class FireIncidentPresentation : IFireIncidentPresentation
    {
        private IFireIncidentRepository _fireIncidentRepository { get; set; }
        private IFiremanTeamRepository _firemanTeamRepository { get; set; }
        private IMapper _mapper { get; set; }

        public FireIncidentPresentation(IFireIncidentRepository fireIncidentRepository, IFiremanTeamRepository firemanTeamRepository, IMapper mapper)
        {
            _fireIncidentRepository = fireIncidentRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _mapper = mapper;
        }
        public List<FireIncidentViewModel> GetAllIncidents()
        {
            var viewmodels = _fireIncidentRepository.GetAll()
                   .Select(x => _mapper.Map<FireIncidentViewModel>(x)).ToList();
            return viewmodels;
        }
        public void AddFireIncident(FireIncidentViewModel model)
        {
            var newModel = _mapper.Map<FireIncident>(model);
            if (string.IsNullOrEmpty(model.TeamName))
            {
                var team = _firemanTeamRepository.GetFreeTeam();
                if (team != null)
                {
                    team.TeamState = TeamState.Busy;
                    newModel.FiremanTeam = team;
                }
                newModel.Reason = "Not defined";
                newModel.Status = IncidentStatus.Fire;
                newModel.Date = DateTime.Now;
                _firemanTeamRepository.Save(team);
            }
            else
            {
                var team = _firemanTeamRepository.GetByName(model.TeamName);
                newModel.FiremanTeam = team;
            }

			_fireIncidentRepository.Save(newModel);
		}
		public bool Remove(long id)
		{
			var fireman = _fireIncidentRepository.Get(id);
			if (fireman == null)
			{
				return false;
			}

			_fireIncidentRepository.Remove(fireman);
			return true;
		}
		public FireIncidentViewModel GetIncident(long id)
		{
			var fireincident = _fireIncidentRepository.Get(id);
			var model = _mapper.Map<FireIncidentViewModel>(fireincident);

            model.TeamName = fireincident.FiremanTeam?.TeamName;
            return model;
        }
        public void Edit(FireIncidentViewModel model)
        {
            var incident = _fireIncidentRepository.Get(model.Id);
            if (incident != null)
            {
                incident.Address = model.Address;
                incident.Date = model.Date;
                incident.Reason = model.Reason;
                incident.Injured = model.Injured;
                incident.Dead = model.Dead;
                incident.Date = model.Date;
                incident.Status = model.Status;

				var team = _firemanTeamRepository.GetByName(model.TeamName);

				incident.TeamId = team.Id;
				incident.FiremanTeam = team;

				_fireIncidentRepository.Save(incident);
			}
		}
	}

}
