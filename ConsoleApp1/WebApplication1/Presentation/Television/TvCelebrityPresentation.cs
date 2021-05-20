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
    public class TvCelebrityPresentation
    {
        private IMapper _mapper { get; set; }
        private IUserService _userService { get; set; }
        private ICitizenRepository _citizenRepository { get; set; }
        private TvCelebrityRepository _celebrityRepository { get; set; }
        private TvProgrammeCelebrityRepository _programmeCelebrityRepository { get; set; }
        private TvProgrammeRepository _programmeRepository { get; set; }
        public TvCelebrityPresentation(IMapper mapper, IUserService userService, ICitizenRepository citizenRepository,
                                        TvCelebrityRepository celebrityRepository, TvProgrammeCelebrityRepository programmeCelebrityRepository,
                                        TvProgrammeRepository programmeRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _citizenRepository = citizenRepository;
            _celebrityRepository = celebrityRepository;
            _programmeCelebrityRepository = programmeCelebrityRepository;
            _programmeRepository = programmeRepository;
        }

        public List<TvCelebrityViewModel> GetIndexViewModel()
        {
            return _celebrityRepository.GetAll().Select(_mapper.Map<TvCelebrityViewModel>).ToList();
        }

        public List<FullProfileViewModel> AreNotCelebrity()
        {
            return _citizenRepository.AreNotCelebrity().Select(_mapper.Map<FullProfileViewModel>).ToList();
        }

        public void Save(TvCelebrityViewModel viewModel)
        {
            var citizen = _citizenRepository.GetByName(viewModel.Citizen.Name);

            var model = _mapper.Map<TvCelebrity>(viewModel);
            model.Citizen = citizen;
            _celebrityRepository.Save(model);
        }

        public TvProgrammeCelebrityViewModel PutCelebrityToProgramme(long id)
        {
            var programme = _mapper.Map<TvProgrammeShortViewModel>(_programmeRepository.Get(id));
            var viewModel = new TvProgrammeCelebrityViewModel()
            {
                Programme = programme
            };
            return viewModel;
        }

        public void SavePutCelebrityToProgramme(TvProgrammeCelebrityViewModel viewModel)
        {
            var celebrity = _celebrityRepository.GetByName(viewModel.Celebrity.Name);
            var programme = _programmeRepository.GetByName(viewModel.Programme.Name);

            var model = _mapper.Map<TvProgrammeCelebrity>(viewModel);
            model.Celebrity = celebrity;
            model.Programme = programme;

            _programmeCelebrityRepository.Save(model);
        }

        public List<TvProgrammeCelebrityViewModel> GetCelebrityByProgramme(string programmeName)
        {
            return _programmeCelebrityRepository.GetByProgrammeName(programmeName).Select(_mapper.Map<TvProgrammeCelebrityViewModel>).ToList();
        }

    }
}
