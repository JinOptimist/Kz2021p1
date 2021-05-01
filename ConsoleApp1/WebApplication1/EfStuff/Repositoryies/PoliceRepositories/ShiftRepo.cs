using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class ShiftRepo : BaseRepository<Shift>, IShiftRepo
	{
		public ShiftRepo(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
