using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class TripRouteRepository : BaseRepository<TripRoute>
    {
        public TripRouteRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
        
        public TripRoute GetByTitle(string title)
        {
            return _kzDbContext.TripRoute.SingleOrDefault(x => x.Title == title);
        }
        
    }
}
