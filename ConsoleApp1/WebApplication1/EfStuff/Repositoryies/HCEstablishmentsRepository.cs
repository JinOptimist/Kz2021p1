using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class HCEstablishmentsRepository : BaseRepository<HCEstablishments>, IHCEstablishmentsRepository
    {
        public HCEstablishmentsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public HCEstablishments GetByName(string name)
        {
            return _kzDbContext.HCEstablishments.FirstOrDefault(x => x.Name == name);
        }
    }
}
