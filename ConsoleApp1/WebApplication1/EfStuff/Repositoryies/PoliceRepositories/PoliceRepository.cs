using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class PoliceRepository : BaseRepository<Policeman>
    {
        public PoliceRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}
