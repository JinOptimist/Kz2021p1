using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class UniversityRepository : BaseRepository<University>, IUniversityRepository
    {
        public UniversityRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public University GetUniversityByName(string name)
        {
            return _kzDbContext.Universities.SingleOrDefault(x => x.Name.Equals(name));
        }
    }
}
