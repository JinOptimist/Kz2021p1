using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
	public class FlightsRepository : BaseRepository<Flight>, IFlightsRepository
    {
        public FlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
