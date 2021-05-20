using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class ElectionRepository : BaseRepository<Election>, IElectionRepository
    {
        public ElectionRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}