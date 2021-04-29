using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;

namespace WebApplication1.EfStuff.Repositoryies.FiremanRepo
{
    public class FireIncidentRepository : BaseRepository<FireIncident>
    {
        public FireIncidentRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
