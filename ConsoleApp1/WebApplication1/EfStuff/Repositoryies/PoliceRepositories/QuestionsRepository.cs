using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.PoliceRepositories.Interfaces;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class QuestionsRepository : BaseRepository<PoliceQuizQuestion>, IQuestionsRepository
	{
		public QuestionsRepository(KzDbContext kzDbContext) : base(kzDbContext) 
		{

		}
    }
}
