using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class TripRouteRepository : BaseRepository<TripRoute>, ITripRouteRepository
    {
        public TripRouteRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
        
        public TripRoute GetByTitle(string title)
        {
            return _kzDbContext.TripRoute.SingleOrDefault(x => x.Title == title);
        }
        
    }
}
