using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;

namespace WebApplication1.EfStuff.Repositoryies.FiremanRepo
{
    public class FireIncidentRepository : BaseRepository<FireIncident>, IFireIncidentRepository
    {
        public FireIncidentRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
