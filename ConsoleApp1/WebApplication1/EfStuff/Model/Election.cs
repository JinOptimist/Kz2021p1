using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Election : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public virtual ICollection<CandidateElection> CandidateElections { get; set; }
        public virtual ICollection<ElectionBallot> ElectionBallots { get; set; }
     
    }

    public class CandidateElection : BaseModel
    {
        public long CandidateId { get; set; }
     
        public  virtual Candidate Candidate { get; set; }
        public long ElectionId { get; set; }
        
        public  virtual Election Election { get; set; }
            
    }

    public class ElectionBallot  : BaseModel

    {
        public long BallotId { get; set; }
        
        public  virtual Ballot Ballot { get; set; }
        
        public long ElectionId { get; set; }
        
        public  virtual Election Election { get; set; }
    }
}
