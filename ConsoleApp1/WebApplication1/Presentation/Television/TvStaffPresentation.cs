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
using WebApplication1.Services;

namespace WebApplication1.Presentation.Television
{
    public class TvStaffPresentation
    {
        private TvStaffRepository _staffRepository { get; set; }
        private TvProgrammeStaffRepository _programmeStaffRepository { get; set; }
        private IMapper _mapper { get; set; }
        private IUserService _userService { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private TvProgrammeRepository _programmeRepository { get; set; }

        public TvStaffPresentation(TvStaffRepository staffRepository, TvProgrammeStaffRepository programmeStaffRepository,
                                    IMapper mapper, IUserService userService, ICitizenRepository citizenRepository,
                                    TvProgrammeRepository programmeRepository)
        {
            _staffRepository = staffRepository;
            _programmeStaffRepository = programmeStaffRepository;
            _mapper = mapper;
            _userService = userService;
            _citizenRepository = citizenRepository;
            _programmeRepository = programmeRepository;
        }

        public List<TvStaffViewModel> GetIndexViewModelByChannel(string channelName)
        {
            return _staffRepository
                 .GetByChannel(channelName)
                 .Select(x => _mapper.Map<TvStaffViewModel>(x))
                 .ToList();
        }

        public TvStaffViewModel GetProfileViewModel()
        {
            var staff = _staffRepository.Get(_userService.GetUser().TvStaff.Id);
            var staffViewModel = _mapper.Map<TvStaffViewModel>(staff);
            return staffViewModel;
        }

        public List<FullProfileViewModel> AreNotTvStaff()
        {
            return _citizenRepository.AreNotTvStaff().Select(_mapper.Map<FullProfileViewModel>).ToList();
        }

        public void Save(TvStaffViewModel viewModel)
        {
            var model = _mapper.Map<TvStaff>(viewModel);
            var citizen = _citizenRepository.GetByName(viewModel.Citizen.Name);
            model.Citizen = citizen;
            model.Channel = _userService.GetUser().TvStaff.Channel;
            _staffRepository.Save(model);
        }

        public List<TvStaffViewModel> GetStaffByChannel()
        {
            var channelName = _userService.GetUser().TvStaff.Channel.Name;
            var staff= _staffRepository.GetByChannel(channelName).Select(_mapper.Map<TvStaffViewModel>).ToList();
            return staff;
        }

        public TvProgrammeStaffViewModel PutStaffToProgramme(long id)
        {
            var programme = _mapper.Map<TvProgrammeShortViewModel>(_programmeRepository.Get(id));
            var viewModel = new TvProgrammeStaffViewModel()
            {
                Programme = programme
            };
            return viewModel;
        }

        public void SavePutStaffToProgramme(TvProgrammeStaffViewModel viewModel)
        {
            var staff = _staffRepository.GetByName(viewModel.Staff.Name);
            var programme = _programmeRepository.GetByName(viewModel.Programme.Name);

            var model = _mapper.Map<TvProgrammeStaff>(viewModel);
            model.Staff = staff;
            model.Programme = programme;

            _programmeStaffRepository.Save(model);
        }
        public List<TvProgrammeStaffViewModel> GetStaffByProgramme(string programmeName)
        {
            return _programmeStaffRepository
                    .GetByProgrammeName(programmeName)
                    .Select(_mapper.Map<TvProgrammeStaffViewModel>)
                    .ToList();
        }

        public List<TvProgrammeStaffViewModel> GetProgrammeByStaff(string name)
        {
            return _programmeStaffRepository.GetByStaffName(name).Select(_mapper.Map<TvProgrammeStaffViewModel>).ToList();
        }
    }
}
