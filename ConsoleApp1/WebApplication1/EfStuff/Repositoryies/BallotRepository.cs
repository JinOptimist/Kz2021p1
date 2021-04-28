namespace WebApplication1.EfStuff.Repositoryies
{
    public class BallotRepository : BaseRepository<Ballot>
    {
        public BallotRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
    }
}
        
    
        