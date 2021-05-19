using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;

namespace WebApplication1.EfStuff.Repositoryies.FiremanRepo
{
    public class FiremanRepository : BaseRepository<Fireman>, IFiremanRepository
    {
        public FiremanRepository(KzDbContext kzDbContext):base(kzDbContext)    
        {
        }

        public Fireman GetByName(string name)
        {
            return _kzDbContext.Firemen.SingleOrDefault(x => x.Citizen.Name == name);
        }
    }
}
