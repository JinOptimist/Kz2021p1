using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;

namespace WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface
{
    public interface IFiremanRepository : IBaseRepository<Fireman>
    {
        Fireman GetByName(string name);
    }
}
