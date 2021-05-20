using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
    public interface ICitizenRepository : IBaseRepository<Citizen>
    {
        Citizen GetByName(string name);

        Citizen Login(string name, string password);

        List<Citizen> AreNotTvStaff();
        List<Citizen> AreNotCelebrity();
    }
}
