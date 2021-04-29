using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class BusRepository : BaseRepository<Bus>
    {
        public BusRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
        
        public Bus GetByModel(string model)
        {
            return _kzDbContext.Buses.SingleOrDefault(x => x.Model == model);            
        }

        public Bus GetById(long id)
        {
            return _kzDbContext.Buses.SingleOrDefault(x => x.Id == id);
        }

    }
}
