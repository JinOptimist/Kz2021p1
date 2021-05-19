using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
        public BusRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Bus GetByModel(string model)
        {
            return _kzDbContext.Buses.FirstOrDefault(x => x.Model == model);
        }

        public Bus GetByType(string type)
        {
            return _kzDbContext.Buses.FirstOrDefault(x => x.Type == type);
        }



        public Bus GetById(long id)
        {
            return _kzDbContext.Buses.SingleOrDefault(x => x.Id == id);
        }

        public long FreeBusId(string model, string routeTitle)
        {
            var freeBus = _kzDbContext.Buses.FirstOrDefault(x => x.Model == model && x.RoutePlan.Title == routeTitle && x.IsOnOrder == false);
            return freeBus.Id;
        }

        public bool IsFree(string model, string routeTitle)
        {
            return _kzDbContext.Buses.Any(x => x.Model == model && x.RoutePlan.Title == routeTitle && x.IsOnOrder == false);
        }

        public void UpdateBusRoute(TripRoute route)
        {
            _kzDbContext.Buses.Where(x => x.RoutePlan == route).ToList().ForEach(x => x.RoutePlan = new TripRoute()
            {
                Title = "Daily",
                Type = "ordinary",
                Length = 1,
                TripTime = 1,
                Price = 1,
                Buses = null
            });
            _kzDbContext.SaveChanges();
        }

        public void UpdateBusOrderStatus(long id, bool flag)
        {
            _kzDbContext.Buses.Find(id).IsOnOrder = flag;
            _kzDbContext.SaveChanges();
        }

    }
}