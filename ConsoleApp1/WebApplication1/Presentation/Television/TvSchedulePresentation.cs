using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models.Television;
using WebApplication1.Services;

namespace WebApplication1.Presentation.Television
{
    public class TvSchedulePresentation
    {
        private TvScheduleRepository _scheduleRepository { get; set; }
        public IMapper _mapper { get; set; }
        public TvProgrammeRepository _programmeRepository { get; set; }
        public IUserService _userService { get; set; }
        public TvSchedulePresentation(TvScheduleRepository scheduleRepository, IMapper mapper, TvProgrammeRepository programmeRepository, 
                                        IUserService userService)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _programmeRepository = programmeRepository;
            _userService = userService;
        }

        public List<TvScheduleViewModel> GetIndexViewModel(string channelName, string date)
        {
            var dateTime = Convert.ToDateTime(date);

            var schedules = _scheduleRepository
                .GetByChannelAndDate(channelName, dateTime)
                .Select(x => _mapper.Map<TvScheduleViewModel>(x))
                .ToList();
            return schedules;
        }

        public List<TvProgrammeShortViewModel> GetProgrammesViewModel()
        {
            var channelName = _userService.GetUser().TvStaff.Channel.Name;
            var programmes = _programmeRepository.GetByChannel(channelName)
                .Select(_mapper.Map<TvProgrammeShortViewModel>).ToList();
            return programmes;
        }

        public void Save(TvScheduleViewModel viewModel)
        {
            var programme = _programmeRepository.Get(viewModel.Programme.Id);
            viewModel.Programme = _mapper.Map<TvProgrammeShortViewModel>(programme);
            var model = _mapper.Map<TvSchedule>(viewModel);
            model.Programme = programme;
            _scheduleRepository.Save(model);
        }

        public TvScheduleViewModel Find(long id)
        {
            return _mapper.Map<TvScheduleViewModel>(_scheduleRepository.Get(id));
        }

        public void Edit(TvScheduleViewModel viewModel)
        {
            var programme = _programmeRepository.Get(viewModel.Programme.Id);
            viewModel.Programme = _mapper.Map<TvProgrammeShortViewModel>(programme);
            var model = _mapper.Map<TvSchedule>(viewModel);
            model.Programme = programme;
            _scheduleRepository.Save(model);
        }

        public bool Delete(long id)
        {
            var schedule = _scheduleRepository.Get(id);
            if (schedule == null)
            {
                return false;
            }

            _scheduleRepository.Remove(schedule);

            return true;
        }
    }
}
