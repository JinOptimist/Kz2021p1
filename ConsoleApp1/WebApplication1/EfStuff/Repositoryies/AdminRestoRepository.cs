using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class AdminRestoRepository : BaseRepository<AdminResto>, IAdminRestoRepository
    {
        public AdminRestoRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public AdminResto GetByName(string name)
        {
            return _kzDbContext.AdminResto.SingleOrDefault(x => x.LoginAdmin == name);
        }

        public AdminResto Login(string name, string password)
        {
            return _kzDbContext.AdminResto
                .SingleOrDefault(x => x.LoginAdmin == name && x.PasswordAdmin == password);
        }
    }
}
