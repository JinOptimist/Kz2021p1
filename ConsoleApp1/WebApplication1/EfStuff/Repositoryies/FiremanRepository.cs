using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class FiremanRepository : BaseRepository<Fireman>, IFiremanRepository
    {
        public FiremanRepository(KzDbContext kzDbContext):base(kzDbContext)    
        {
        }      
    }
}
