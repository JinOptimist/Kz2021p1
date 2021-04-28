using System;
using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff
{
    public class Ballot : BaseModel
    {
        public virtual Citizen Citizen { get; set; }
        
        public long ElectionId { get; set; }
        
        public long CitizenId { get; set; }
        
        public virtual Candidate Candidate {get; set;}
        
        public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();
       
        public DateTime VoteTime { get; set; }

    }
}