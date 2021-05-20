using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class UniversityRepository : BaseRepository<University>, IUniversityRepository
    {
        public UniversityRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public University GetUniversityByName(string name)
        {
            return _kzDbContext.Universities.SingleOrDefault(x => x.Name == name);
        }
        public List<string> GetUniversityNames()
        {
            return _kzDbContext.Universities.Select(x => x.Name).ToList();
        }

        public List<long> GetUniversityIds()
        {
            return _kzDbContext.Universities.Select(x => x.Id).ToList();
        }
    }
}
