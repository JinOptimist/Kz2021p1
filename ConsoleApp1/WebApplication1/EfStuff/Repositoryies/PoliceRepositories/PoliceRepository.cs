using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceRepository : BaseRepository<Policeman>
    {
        public PoliceRepository(KzDbContext kzDbContext) : base(kzDbContext) { }
    }
}
