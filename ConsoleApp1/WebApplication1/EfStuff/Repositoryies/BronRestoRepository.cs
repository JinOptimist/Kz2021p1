using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class BronRestoRepository : BaseRepository<BronResto>
    {
        public BronRestoRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public BronResto GetByName(string name)
        {
            return _kzDbContext.BronResto.SingleOrDefault(x => x.Restoranses.Name == name);
        }
        public BronResto GetByBrNumber(int number)
        {
            return _kzDbContext.BronResto.FirstOrDefault(x => x.BronRespNumber == number);
        }
    }
}
