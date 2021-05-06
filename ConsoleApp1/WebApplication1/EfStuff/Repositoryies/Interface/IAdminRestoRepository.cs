using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
    public interface IAdminRestoRepository : IBaseRepository<AdminResto>
    {
        AdminResto GetByName(string name);

        AdminResto Login(string name, string password);
    }
}
