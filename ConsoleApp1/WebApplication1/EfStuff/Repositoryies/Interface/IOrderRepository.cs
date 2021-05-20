using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order GetByName(string name);
        Order GetById(long id);
    }
}