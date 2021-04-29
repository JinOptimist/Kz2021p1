using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;

namespace WebApplication1.EfStuff.Repositoryies.FiremanRepo
{
    public class FireTruckRepository : BaseRepository<FireTruck>
    {
        public FireTruckRepository(KzDbContext kzDbContext) : base(kzDbContext)
        { }
        public FireTruck GetByNumber(string number)
        {
            return _kzDbContext.FireTrucks.SingleOrDefault(x => x.TruckNumber == number);
        }
    }
}
