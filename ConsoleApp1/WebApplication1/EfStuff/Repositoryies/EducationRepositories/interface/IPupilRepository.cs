using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface IPupilRepository : IBaseRepository<Pupil>
    {
        Pupil GetPupilByIin(string pupilIin);
        List<Pupil> GetPupilsWithEnt();
    }
}
