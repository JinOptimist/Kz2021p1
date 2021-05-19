using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class TripRouteRepository : BaseRepository<TripRoute>, ITripRouteRepository
    {
        public TripRouteRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public TripRoute GetByType(string type)
        {
            return _kzDbContext.TripRoute.FirstOrDefault(x => x.Type == type);
        }

        public TripRoute GetByTitle(string title)
        {
            return _kzDbContext.TripRoute.FirstOrDefault(x => x.Title == title);
        }
        public TripRoute GetById(long id)
        {
            return _kzDbContext.TripRoute.SingleOrDefault(x => x.Id == id);
        }

    }
}