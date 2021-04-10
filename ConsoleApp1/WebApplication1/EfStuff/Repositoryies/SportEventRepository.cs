using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class SportEventRepository : BaseRepository<SportEvent>
    {
        public SportEventRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {

        }
    }
}
