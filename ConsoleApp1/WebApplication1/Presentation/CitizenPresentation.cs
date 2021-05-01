using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Presentation
{
	public class CitizenPresentation : ICitizenPresentation
	{
		private ICitizenRepository _citizenRepository;
		private IUserService _userService;
		private IMapper _mapper;

		public CitizenPresentation(ICitizenRepository citizenRepository,
			IUserService userService, IMapper mapper)
		{
			_citizenRepository = citizenRepository;
			_userService = userService;
			_mapper = mapper;
		}

		public List<FullProfileViewModel> GetIndexViewModel()
		{
			return _citizenRepository.GetAll()
				.Select(x => new FullProfileViewModel()
				{
					Age = x.Age,
					Name = x.Name,
					RegistrationDate = x.CreatingDate
				}).ToList();
		}

		public ClaimsPrincipal GetClaimsPrincipal(Citizen user)
		{
			var pages = new List<Claim>() {
				new Claim("Id", user.Id.ToString()),
				new Claim("Name", user.Name.ToString()),
				new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthMethod)
			};

			var claimsIdenity = new ClaimsIdentity(pages, Startup.AuthMethod);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdenity);
			return claimsPrincipal;
		}

		public void Save(LoginViewModel viewModel)
		{
			var citizen = new Citizen()
			{
				Name = viewModel.Name,
				Password = viewModel.Password
			};

			_citizenRepository.Save(citizen);
		}

		public FullProfileViewModel FullProfile()
		{
			var user = _userService.GetUser();

			var viewModel = _mapper.Map<FullProfileViewModel>(user);

			return viewModel;
		}

		public bool Remove(string name)
		{
			var citizen = _citizenRepository.GetByName(name);
			if (citizen == null)
			{
				return false;
			}

			_citizenRepository.Remove(citizen);

			return true;
		}
	}
}
