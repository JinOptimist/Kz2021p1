using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class PupilRepository : BaseRepository<Pupil>
    {
        public PupilRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Pupil GetPupilByIIN(string pupilIIN)
        {
            return _kzDbContext.Pupils.SingleOrDefault(x => x.IIN.Equals(pupilIIN));
        }
    }
}
