using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Ballot : BaseModel
    {
        public DateTime VoteTime  {get; set;}
        
        public long CandidateId {get; set; }
        public virtual Candidate Candidate { get; set; }

        public virtual Citizen Citizen { get; set; }
        public virtual ICollection<ElectionBallot> ElectionBallots { get; set; }
    }
}
