using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Presentation
{
    public class HCEstablishmentsPresentation
    {
        private IHCEstablishmentsRepository _hcestablishmentsRepository;
        private IUserService _userService;
        private IMapper _mapper;

        public HCEstablishmentsPresentation(IHCEstablishmentsRepository establishmentsRepository, IUserService userService, IMapper mapper)
        {
            _hcestablishmentsRepository = establishmentsRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public List<HCEstablishmentsViewModel> GetIndexViewModel()
        {
            return _hcestablishmentsRepository
              .GetAll()
              .Select(x => _mapper.Map<HCEstablishmentsViewModel>(x))
              .ToList();
        }

        public HCEstablishmentsViewModel CreateEStablishmentsViewModel()
        {
            var user = _userService.GetUser();
            return _mapper.Map<HCEstablishmentsViewModel>(user);
        }

        public bool Remove(long id)
        {
            var userForDelete = _hcestablishmentsRepository.Get(id);
            if (userForDelete == null)
            {
                return false;
            }

            _hcestablishmentsRepository.Remove(userForDelete);

            return true;
        }
    }
}
