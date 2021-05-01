using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PupilRepository : BaseRepository<Pupil>, IPupilRepository
    {
        public PupilRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Pupil GetPupilByIIN(string pupilIIN)
        {
            return _kzDbContext.Pupils.SingleOrDefault(x => x.IIN.Equals(pupilIIN));
        }
    }
}
