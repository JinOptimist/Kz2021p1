using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class CitizenPresentation
    {
        private CitizenRepository _citizenRepository;
        private KzDbContext _kzDbContext;

        public CitizenPresentation(CitizenRepository citizenRepository, KzDbContext kzDbContext)
        {
            _citizenRepository = citizenRepository;
            _kzDbContext = kzDbContext;
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

        public bool IsValidSave(LoginViewModel viewModel)
        {
            var citizen = new Citizen()
            {
                Name = viewModel.Name,
                Password = viewModel.Password
            };
            if (CandidateExist(citizen))
            {
             return false;   
            }
            _citizenRepository.Save(citizen);
            return true;
        }
        
        public bool CandidateExist(Citizen citizen) => _kzDbContext.Citizens.Any(c => c.Name == citizen.Name);

    }
}
