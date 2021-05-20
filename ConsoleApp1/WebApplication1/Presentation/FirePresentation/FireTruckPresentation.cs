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
    public class FireTruckPresentation : IFireTruckPresentation
    {
        private IFireTruckRepository _fireTruckRepository { get; set; }
        private IFiremanTeamRepository _firemanTeamRepository { get; set; }
        private IMapper _mapper { get; set; }
        public FireTruckPresentation(IFiremanTeamRepository firemanTeamRepository, IFireTruckRepository fireTruckRepository, IMapper mapper)
        {
            _fireTruckRepository = fireTruckRepository;
            _firemanTeamRepository = firemanTeamRepository;
            _mapper = mapper;
        }
        public List<FireTruckViewModel> GetAllTrucks()
        {
            var viewmodels = _fireTruckRepository.GetAll()
               .Select(x => new FireTruckViewModel()
               {
                   Id = x.Id,
                   TruckNumber = x.TruckNumber,
                   TruckState = x.TruckState,
                   TeamName = x.FiremanTeam?.TeamName
               }).ToList();
            return viewmodels;
        }
        public void AddFireTruck(FireTruckViewModel model)
        {
            var newModel = new FireTruck()
            {
                TruckNumber = model.TruckNumber,
                TruckState = model.TruckState,
            };
            if (model.TeamName == "0")
            {
                newModel.FiremanTeam = null;
            }
            else
            {
                var team = _firemanTeamRepository.GetByName(model.TeamName);
                newModel.FiremanTeam = team;

			}
			_fireTruckRepository.Save(newModel);
		}
		public bool Remove(long id)
		{
			var model = _fireTruckRepository.Get(id);
			if (model == null)
			{
				return false;
			}
			var fireman = _firemanTeamRepository.Get(model.FiremanTeam.Id);
			fireman.TruckId = null;
			fireman.FireTruck = null;
			_firemanTeamRepository.Save(fireman);
			_fireTruckRepository.Remove(model);
			return true;
		}
		public FireTruckViewModel GetTruck(long id)
		{
			var firetruck = _fireTruckRepository.Get(id);
			var model = _mapper.Map<FireTruckViewModel>(firetruck);

			model.TeamName = firetruck.FiremanTeam?.TeamName;
			return model;
		}
		public void Edit(FireTruckViewModel model)
		{
			var firetruck = _fireTruckRepository.Get(model.Id);
			if (firetruck != null)
			{
				firetruck.TruckNumber = model.TruckNumber;
				firetruck.TruckState = model.TruckState;
				firetruck.FiremanTeam = _firemanTeamRepository.GetByName(model.TeamName);

				_fireTruckRepository.Save(firetruck);
			}
		}
	}
}
