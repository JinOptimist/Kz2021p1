using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceCallRepository : BaseRepository<PoliceCallHistory>, IPoliceCallRepository
	{
		public PoliceCallRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
	}
}
