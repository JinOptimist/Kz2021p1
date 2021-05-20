using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Order GetByName(string name) => _kzDbContext.Orders.SingleOrDefault(x => x.Name == name);        
        public Order GetById(long id) => _kzDbContext.Orders.SingleOrDefault(x => x.Id == id);       
    }
}