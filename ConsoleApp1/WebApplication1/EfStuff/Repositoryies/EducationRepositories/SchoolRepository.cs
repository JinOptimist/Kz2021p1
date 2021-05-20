using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {
        public SchoolRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
        public School GetSchoolByName(string name)
        {
            return _kzDbContext.Schools.SingleOrDefault(x => x.Name == name);
        }
        public List<string> GetSchoolNames()
        {
            return _kzDbContext.Schools.Select(x => x.Name).ToList();
        }
    }
}
