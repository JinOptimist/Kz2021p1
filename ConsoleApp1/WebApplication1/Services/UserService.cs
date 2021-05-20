using Microsoft.AspNetCore.Http;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.Services
{
	public class UserService : IUserService
    {
        private ICitizenRepository _citizenRepository;

        private IHttpContextAccessor _httpContextAccessor;

        public UserService(ICitizenRepository citizenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _citizenRepository = citizenRepository;
            _httpContextAccessor = httpContextAccessor;
        }

		public Citizen GetUser()
		{
			var idStr = _httpContextAccessor
				.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
			if (string.IsNullOrEmpty(idStr))
			{
				return null;
			}

			var id = int.Parse(idStr);
			return _citizenRepository.Get(id);
		}


		public bool IsPolicmen()
			=> GetUser()?.Policeman != null;

		public bool IsActiveDuty()
		{
			Citizen user = GetUser();

			return user?.Policeman != null && user?.Policeman.Rank != Rank.Trainee;
		}

		public bool IsSheriff()
			=> GetUser()?.Policeman.Rank == Rank.Sheriff;
		public bool IsOfficer()
			=> GetUser()?.Policeman.Rank == Rank.Officer;
		public bool IsTrainee()
			=> GetUser()?.Policeman.Rank == Rank.Trainee;
        public bool IsFireAdmin()
            => GetUser()?.Fireman?.Role == EfStuff.Model.Firemen.FireWorkerRole.Fireadmin;
        public bool IsTruckSpecialist()
           => GetUser()?.Fireman?.Role == EfStuff.Model.Firemen.FireWorkerRole.FireTruckSpecialist;

        public bool IsHCWorker() 
            => GetUser()?.HCWorker != null;
    }
}
