using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class AdressRepository : BaseRepository<Adress>, IAdressRepository
    {
        public AdressRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
