using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class SportEventRepository : BaseRepository<SportEvent>, ISportEventRepository
    {
        public SportEventRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {

        }
    }
}
