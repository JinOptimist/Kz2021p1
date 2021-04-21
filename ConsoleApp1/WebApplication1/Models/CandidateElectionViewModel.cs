using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class CandidateElectionViewModel
    {
      
        public Citizen Citizen { get; set; }
        public Election Election {get; set;}
        public IList<CandidateElection> Candidates { get; set; }
    }
}