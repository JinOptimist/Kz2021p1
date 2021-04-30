using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class CitizenPresentation
    {
        private CitizenRepository _citizenRepository;

        public CitizenPresentation(CitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
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
    }
}
