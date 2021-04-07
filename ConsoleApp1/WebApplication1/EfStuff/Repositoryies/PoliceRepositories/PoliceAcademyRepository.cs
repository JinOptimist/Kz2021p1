using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceAcademyRepository : BaseRepository<PoliceAcademy>
    {
		public PoliceAcademyRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
