using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class CitizenRepository : BaseRepository<Citizen>, ICitizenRepository
    {
        public CitizenRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Citizen GetByName(string name)
        {
            return _kzDbContext.Citizens.SingleOrDefault(x => x.Name == name);
        }

        public Citizen Login(string name, string password)
        {
            return _kzDbContext.Citizens
                .SingleOrDefault(x => x.Name == name && x.Password == password);
        }
    }
}
