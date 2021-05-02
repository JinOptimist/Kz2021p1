using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface IBusRepository : IBaseRepository<Bus>
    {
        Bus GetByModel(string model);
        Bus GetById(long id);
    }
}
