using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class ViolationsRepository : BaseRepository<Violations>
    {
		public ViolationsRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    } 
}
