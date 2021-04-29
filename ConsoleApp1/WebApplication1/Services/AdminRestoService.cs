using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;

namespace WebApplication1.Services
{
    public class AdminRestoService
    {
        private AdminRestoRepository _adminRestoRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public AdminRestoService(AdminRestoRepository adminRestoRepository, IHttpContextAccessor httpContextAccessor)
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
        public ClaimsPrincipal GetClaimsPrincipal()
        {
            return _httpContextAccessor.HttpContext.User;
        }
    }
}
