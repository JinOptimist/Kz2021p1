using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class RestoransRepository : BaseRepository<Restorans>
    {
        public RestoransRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public Restorans GetByName(string name)
        {
            return _kzDbContext.Restorans.SingleOrDefault(x => x.Name == name);
        }
    }
}
