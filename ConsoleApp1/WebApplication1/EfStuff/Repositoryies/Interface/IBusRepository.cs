using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
    public interface IBusRepository : IBaseRepository<Bus>
    {
        Bus GetByModel(string model);
        Bus GetByType(string type);
        Bus GetById(long id);
        bool IsFree(string model, string routeTitle);
        long FreeBusId(string model, string routeTitle);
        void UpdateBusRoute(TripRoute route);
        void UpdateBusOrderStatus(long id, bool flag);
    }
}
