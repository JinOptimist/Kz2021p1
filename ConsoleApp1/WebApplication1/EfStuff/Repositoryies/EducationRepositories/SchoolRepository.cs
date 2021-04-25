using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class SchoolRepository : BaseRepository<School>
    {
        public SchoolRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
        public School GetSchoolByName(string name)
        {
            return _kzDbContext.Schools.SingleOrDefault(x => x.Name.Equals(name));
        }
    }
}
