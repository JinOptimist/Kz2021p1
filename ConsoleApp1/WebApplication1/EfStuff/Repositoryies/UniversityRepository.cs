using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class UniversityRepository : BaseRepository<University>, IUniversityRepository
    {
        public UniversityRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}
