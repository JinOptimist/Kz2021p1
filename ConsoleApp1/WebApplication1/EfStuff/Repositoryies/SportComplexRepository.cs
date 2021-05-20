using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class SportComplexRepository : BaseRepository<SportComplex>, ISportComplexRepository
    {
        public SportComplexRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
