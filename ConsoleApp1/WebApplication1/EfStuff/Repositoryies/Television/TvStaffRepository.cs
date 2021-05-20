using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.EfStuff.Repositoryies.Television
{
    public class TvStaffRepository : BaseRepository<TvStaff>
    {
        public TvStaffRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public List<TvStaff> GetByChannel(string channelName)
        {
            return _dbSet.Where(x => x.Channel.Name == channelName).ToList();
        }

        public TvStaff GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Citizen.Name == name);
        }

    }
}
