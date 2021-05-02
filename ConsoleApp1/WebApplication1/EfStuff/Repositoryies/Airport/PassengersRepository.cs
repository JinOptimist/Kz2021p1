using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
	public class PassengersRepository : BaseRepository<Passenger>, IPassengersRepository
    {
        public PassengersRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
