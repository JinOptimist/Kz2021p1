using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class HCWorkerRepository : BaseRepository<HCWorker>, IHCWorkerRepository
    {
        public HCWorkerRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public HCWorker GetByName(string name)
        {
            return _kzDbContext.HCWorker.SingleOrDefault(x => x.Name == name);
        }
    }
}
