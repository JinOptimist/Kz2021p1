using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface IUniversityRepository : IBaseRepository<University>
    {
        University GetUniversityByName(string name);
    }
}
