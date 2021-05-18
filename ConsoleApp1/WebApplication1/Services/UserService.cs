using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.EfStuff.Repositoryies;
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

        //public bool IsPolicment()
        //{
        //    return GetUser()?.Policeman == null;
        //}
        public bool IsPolicment()
             => GetUser()?.Policeman != null;

        public bool IsTvStaff() => GetUser()?.TvStaff != null;

        public bool IsTvAdmin() 
        {
            return GetUser()?.TvStaff != null ? GetUser().TvStaff.Occupation == Occupation.TvAdmin : false;
        }

        public bool IsTvDirector()
        {
            return GetUser()?.TvStaff != null ? GetUser().TvStaff.Occupation == Occupation.Director : false;
        }

        public bool IsTvCastingDirector()
        {
            return GetUser()?.TvStaff != null ? GetUser().TvStaff.Occupation == Occupation.CastingDirector : false;
        }
    }
}
