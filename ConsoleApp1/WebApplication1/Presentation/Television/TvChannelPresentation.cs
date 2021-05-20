using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models;
using WebApplication1.Models.Television;

namespace WebApplication1.Presentation.Television
{
    public class TvChannelPresentation
    {
        private TvChannelRepository _channelRepository { get; set; }
        private IMapper _mapper { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private TvStaffRepository _staffRepository { get; set; }

        public TvChannelPresentation(TvChannelRepository channelRepository, IMapper mapper, ICitizenRepository citizenRepository,
                                    TvStaffRepository staffRepository)
        {
            _channelRepository = channelRepository;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
            _staffRepository = staffRepository;
        }

        public List<TvChannelViewModel> GetIndexViewModel()
        {
            return _channelRepository
                .GetAll()
                .Select(_mapper.Map<TvChannelViewModel>)
                .ToList();
        }

        public TvChannelViewModel ProfileViewModel(string channelName)
        {
            return _mapper.Map<TvChannelViewModel>(_channelRepository.GetByName(channelName));
        }

        public bool NameExist(string name)
        {
            return _channelRepository.CheckIfNameExists(name);
        }

        public void Save(TvChannelViewModel viewModel)
        {
            var model = _mapper.Map<TvChannel>(viewModel);
            _channelRepository.Save(model);
        }

        public List<FullProfileViewModel> AreNotTvStaff()
        {
            return _citizenRepository.AreNotTvStaff().Select(_mapper.Map<FullProfileViewModel>).ToList();
        }

        public TvStaffViewModel NewDirectorViewModel(string channelName)
        {
            var channel = _mapper.Map<TvChannelViewModel>(_channelRepository.GetByName(channelName));
            var viewModel = new TvStaffViewModel()
            {
                Channel = channel,
                Occupation = Occupation.Director
            };

            return viewModel;
        }

        public void SaveDirector(TvStaffViewModel viewModel)
        {
            var model = _mapper.Map<TvStaff>(viewModel);

            var citizen = _citizenRepository.GetByName(viewModel.Citizen.Name);
            model.Citizen = citizen;

            var channel = _channelRepository.GetByName(viewModel.Channel.Name);
            model.Channel = channel;

            _staffRepository.Save(model);
        }
    }
}
