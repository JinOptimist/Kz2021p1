using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;

namespace WebApplication1.EfStuff.Repositoryies.FiremanRepo
{
    public class FiremanTeamRepository : BaseRepository<FiremanTeam>, IFiremanTeamRepository
    {
        public FiremanTeamRepository(KzDbContext kzDbContext) : base(kzDbContext)
        { }
        public FiremanTeam GetByName(string name)
        {
            return _kzDbContext.FiremanTeams.SingleOrDefault(x => x.TeamName == name);
        }
        public FiremanTeam GetFreeTeam()
        {
            return _kzDbContext.FiremanTeams.FirstOrDefault(x => x.TeamState == TeamState.Free);
        }
    }
}
