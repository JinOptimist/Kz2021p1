using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class BallotRepository : BaseRepository<Ballot>, IBallotRepository
    {
        public BallotRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}