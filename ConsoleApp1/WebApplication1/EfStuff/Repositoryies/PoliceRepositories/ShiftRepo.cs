using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class ShiftRepo : BaseRepository<Shift>
    {
		public ShiftRepo(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
