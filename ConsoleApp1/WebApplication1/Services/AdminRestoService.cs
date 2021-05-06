using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.Services
{
    public class AdminRestoService
    {
        private IAdminRestoRepository _adminRestoRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public AdminRestoService(IAdminRestoRepository adminRestoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adminRestoRepository = adminRestoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public AdminResto GetUser()
        {
            var idStr = _httpContextAccessor
                .HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            return _adminRestoRepository.Get(id);
        }
        public void AddAdminClaim(ClaimsIdentity ed)
        {
            _httpContextAccessor.HttpContext.User.AddIdentity(ed);
        }

        public ClaimsIdentity GetClaimsIdentity(string stype)
        {
          //  _httpContextAccessor.HttpContext.User.
            return _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault(x=> x.AuthenticationType == stype);
        }
        public ClaimsPrincipal GetClaimsPrincipal()
        {
            return _httpContextAccessor.HttpContext.User;
        }

        public void RemoveAdminClaim()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault(x=> x.AuthenticationType==Startup.AuthAdminR);
            var claim = (from c in _httpContextAccessor.HttpContext.User.Claims
                         where c.Type == "AdminResto"
                         select c).Single();
            identity.RemoveClaim(claim);
        }
    }
}
