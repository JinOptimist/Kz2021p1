using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Services;

namespace WebApplication1.Presentation.FirePresentation
{
    public class FiremanTeamPresentation : IFiremanTeamPresentation
    {
        private IFiremanTeamRepository _firemanTeamRepository { get; set; }
        private IFireTruckRepository _fireTruckRepository { get; set; }
        private IFiremanRepository _firemanRepository { get; set; }
        private IMapper _mapper { get; set; }
        private IUserService _userService { get; set; }


        public FiremanTeamPresentation(IFiremanTeamRepository firemanTeamRepository, IFireTruckRepository fireTruckRepository, IFiremanRepository firemanRepository, IMapper mapper, IUserService userService)
        {
            _firemanTeamRepository = firemanTeamRepository;
            _fireTruckRepository = fireTruckRepository;
            _firemanRepository = firemanRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public List<FiremanTeamViewModel> GetAllTeams()
        {
            var viewModels = _firemanTeamRepository.GetAll()
               .Select(x => _mapper.Map<FiremanTeamViewModel>(x)).ToList();
            return viewModels;
        }
        public void CreateFiremanTeam(FiremanTeamViewModel model)
        {
            var newModel = _mapper.Map<FiremanTeam>(model);
            if (model.TruckId != 0)
            {
                var truck = _fireTruckRepository.Get(model.TruckId);
                newModel.FireTruck = truck;
                truck.FiremanTeam = newModel;
                _fireTruckRepository.Save(truck);
            }
            else 
            {
                newModel.TruckId = null;
            }
            _firemanTeamRepository.Save(newModel);           
        }
        public bool Remove(long id)
        {
            var fireman = _firemanTeamRepository.Get(id);
            if (fireman == null)
            {
                return false;
            }

            _firemanTeamRepository.Remove(fireman);
            return true;
        }
        public FiremanTeamViewModel GetTeam(long id)
        {
            var firemanteam = _firemanTeamRepository.Get(id);
            var model = _mapper.Map<FiremanTeamViewModel>(firemanteam);
            if (firemanteam.FireTruck != null)
            {
                model.TruckState = firemanteam.FireTruck.TruckState;
            }
            model.FiremanCount = firemanteam.Firemen.Count();
            return model;
        }
        public void Edit(FiremanTeamViewModel model)
        {
            var firemanteam = _firemanTeamRepository.Get(model.Id);
            if (firemanteam != null)
            {
                if (_userService.IsFireAdmin())
                {
                    if (model.TruckId != 0)
                    {
                        firemanteam.TruckId = model.TruckId;
                    }
                    firemanteam.TeamName = model.TeamName;
                    firemanteam.TeamState = model.TeamState;
                    firemanteam.FireTruck = _fireTruckRepository.Get(model.TruckId);
                }
                firemanteam.Shift = model.Shift;

                _firemanTeamRepository.Save(firemanteam);
            }
        }
    }
}
