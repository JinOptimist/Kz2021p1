using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceAcademyRepository : BaseRepository<PoliceAcademy>, IPoliceAcademyRepository
	{
		public PoliceAcademyRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
