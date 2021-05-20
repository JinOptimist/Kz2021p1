using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceRepository : BaseRepository<Policeman>, IPoliceRepository
    {
        public PoliceRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}
