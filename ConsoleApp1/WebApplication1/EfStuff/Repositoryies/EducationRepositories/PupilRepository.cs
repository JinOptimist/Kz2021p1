using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PupilRepository : BaseRepository<Pupil>, IPupilRepository
    {
        public PupilRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Pupil GetPupilByIin(string pupilIin)
        {
            return _kzDbContext.Pupils.SingleOrDefault(x => x.Iin.Equals(pupilIin));
        }
    }
}
