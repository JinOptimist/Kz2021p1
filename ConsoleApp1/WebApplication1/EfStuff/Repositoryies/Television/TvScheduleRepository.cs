using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.EfStuff.Repositoryies.Television
{
    public class TvScheduleRepository : BaseRepository<TvSchedule>
    {
        public TvScheduleRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public List<TvSchedule> GetByChannelAndDate(string channelName, DateTime date)
        {
            return _dbSet.Where(x => x.Programme.Channel.Name == channelName && x.AiringTime.Date == date.Date).OrderBy(x => x.AiringTime).ToList();
        }
    }
}
