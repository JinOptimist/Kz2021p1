using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
    public interface ITripRouteRepository : IBaseRepository<TripRoute>
    {
        TripRoute GetByType(string type);
        TripRoute GetByTitle(string title);
        TripRoute GetById(long id);
    }
}