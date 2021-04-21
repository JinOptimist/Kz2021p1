using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class CandidateRepository : BaseRepository<Candidate>
    {
        public CandidateRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Candidate GetByName(string name)
        {
            return _kzDbContext.Candidates.SingleOrDefault(x => x.Name == name);
        }
       
    }
}
