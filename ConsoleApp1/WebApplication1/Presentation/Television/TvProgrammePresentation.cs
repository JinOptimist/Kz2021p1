using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.Models.Television;
using WebApplication1.Services;

namespace WebApplication1.Presentation.Television
{
    public class TvProgrammePresentation
    {
        private TvProgrammeRepository _programmeRepository { get; set; }
        private IMapper _mapper { get; set; }
        private IUserService _userService { get; set; }
        private IWebHostEnvironment _webHostEnvironment { get; set; }
        public TvProgrammePresentation(TvProgrammeRepository programmeRepository, IMapper mapper, IUserService userService,
                                        IWebHostEnvironment webHostEnvironment)
        {
            _programmeRepository = programmeRepository;
            _mapper = mapper;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<TvProgrammeViewModel> GetIndexViewModel()
        {
            return _programmeRepository
                    .GetAll()
                    .Select(_mapper.Map<TvProgrammeViewModel>)
                    .ToList();
        }

        public List<TvProgrammeViewModel> GetListViewModel(string channelName)
        {
            return _programmeRepository
                    .GetByChannel(channelName)
                    .Select(x => _mapper.Map<TvProgrammeViewModel>(x))
                    .ToList();
        }

        public TvProgrammeViewModel GetProfileViewModel(string programmeName)
        {
            var model = _programmeRepository
                        .GetByName(programmeName);
            var viewModel = _mapper.Map<TvProgrammeViewModel>(model);
            return viewModel;
        }

        public async Task<string> UploadAvatar(IFormFile avatarFile)
        {
            var random = DateTime.UtcNow.ToString("yyMMddHHmm");
            var fileExtention = Path.GetExtension(avatarFile.FileName);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(avatarFile.FileName);
            var fileName = $"{random}_{fileNameWithoutExt}{fileExtention}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Image", "Television", fileName);

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await avatarFile.CopyToAsync(fileStream);
            }
            return $"/Image/Television/{fileName}";
        }

        public bool NameExist(string name)
        {
            return _programmeRepository.CheckIfNameExists(name);
        }

        public bool NameExistForEdit(string name, long id)
        {
            return _programmeRepository.CheckIfNameExistsForEdit(name, id);
        }

        public async Task SaveModel(TvProgrammeViewModel viewModel)
        {
            viewModel.AvatarUrl = await UploadAvatar(viewModel.AvatarFile);

            var model = _mapper.Map<TvProgramme>(viewModel);
            model.Channel = _userService.GetUser().TvStaff.Channel;

            _programmeRepository.Save(model);
        }

        public TvProgrammeShortViewModel FindViewModel(long id)
        {
            return _mapper.Map<TvProgrammeShortViewModel>(_programmeRepository.Get(id));
        }

        public void Edit(TvProgrammeShortViewModel viewModel)
        {
            var programme = _mapper.Map<TvProgramme>(viewModel);
            programme.Channel = _userService.GetUser().TvStaff.Channel;
            _programmeRepository.Save(programme);
        }

        public bool Delete(long id)
        {
            var programme = _programmeRepository.Get(id);
            if (programme == null)
            {
                return false;
            }

            _programmeRepository.Remove(programme);

            return true;
        }
    }
}
