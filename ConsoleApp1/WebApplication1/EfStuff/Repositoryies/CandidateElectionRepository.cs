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
        
          public void Add(Candidate candidate, Election election)
                {
                    if (ExistingPair(candidate, election.Id).Count > 0)
                    {
                        throw new Exception("You already registered in this election");
                    }

                    
                    var candidateElection = new CandidateElection
                    {
                        Candidate = candidate,
                        Election = election,
                    };
        
                    _dbSet.Add(candidateElection);
                    _kzDbContext.SaveChanges();
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

        public bool IsRemovedCandidateOfElection(long candidateId, long electionId)
        {
            var existingPair = FindExistingPair(candidateId, electionId).ToList();
            if (existingPair != null)
            {
                
                var candidate = _dbSet.Find(candidateId);
                _dbSet.Remove(candidate);
                _kzDbContext.SaveChanges();
                
                return true;
            }
            return false;
        }
        
        public CandidateElection GetByCandidateId(long candidateId)
        {
            return _dbSet.SingleOrDefault(x => x.CandidateId == candidateId);
        }
    }
}