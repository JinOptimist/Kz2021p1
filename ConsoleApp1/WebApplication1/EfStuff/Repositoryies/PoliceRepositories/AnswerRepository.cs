using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class AnswerRepository : BaseRepository<PoliceQuizAnswer>, IAnswerRepository
    {
		public AnswerRepository(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
