using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class BusRepository : BaseRepository<Bus>, IBusRepository
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
