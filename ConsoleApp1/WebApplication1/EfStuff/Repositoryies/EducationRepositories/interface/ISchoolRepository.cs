using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface ISchoolRepository : IBaseRepository<School>
    {
        School GetSchoolByName(string name);
        List<string> GetSchoolNames();
    }
}
