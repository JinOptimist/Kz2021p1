using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class AnswerRepo : BaseRepository<Answer>
    {
		public AnswerRepo(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}
    }
}
