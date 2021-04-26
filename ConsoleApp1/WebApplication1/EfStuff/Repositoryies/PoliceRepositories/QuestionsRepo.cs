using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class QuestionsRepo : BaseRepository<Question>
    {
		public QuestionsRepo(KzDbContext kzDbContext) : base(kzDbContext) 
		{

		}
    }
}
