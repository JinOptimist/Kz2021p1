using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class CandidateElectionRepository : BaseRepository<CandidateElection>
    {

        public CandidateElectionRepository(KzDbContext kzDbContext) 
            : base(kzDbContext)
        {
        }
        
          public bool Add(Candidate candidate, Election election)
                {
                    if (ExistingPair(candidate, election.Id).Count > 0)
                    {
                        return true;
                    }

                    
                    var candidateElection = new CandidateElection
                    {
                        Candidate = candidate,
                        Election = election,
                    };
        
                    _dbSet.Add(candidateElection);
                    _kzDbContext.SaveChanges();
                    return false;
                }
       
        public  List<CandidateElection> GetCandidatesByElectionId(long id)
        {
           return _dbSet
               .Include(o=> o.Candidate)
               .Where(c => c.ElectionId == id)
               .ToList();
        }

        private bool ElectionExist(long id) => _dbSet.Any(c => c.ElectionId == id);

        private IList<CandidateElection> ExistingPair(Candidate candidate, long electionId)
        {
            
           return  _dbSet
                .Where(ce => ce.Candidate.CitizenId == candidate.CitizenId)
                .Where(ce => ce.ElectionId == electionId)
                .ToList();
        }
        
      

        private IList<CandidateElection> FindExistingPair(long candidate, long electionId)
        {
              return  _dbSet
                 .Where(ce => ce.Candidate.CitizenId == candidate)
                 .Where(ce => ce.ElectionId == electionId)
                 .ToList();
        }

        public bool RemoveCandidateOfElection(long candidateId, long electionId)
        {
            var candidate = _kzDbContext.CandidateElections.FirstOrDefault(
                  c => c.CandidateId == candidateId
                       && c.ElectionId == electionId);
            _dbSet.Remove(candidate);
            _kzDbContext.SaveChanges();

            return true;

        }


    }
}