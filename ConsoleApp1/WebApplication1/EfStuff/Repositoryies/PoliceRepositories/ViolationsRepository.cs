using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class ViolationsRepository : BaseRepository<Violations>, IViolationsRepository
	{
		public ViolationsRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    } 
}
