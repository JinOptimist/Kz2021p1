using System;
using System.Collections.Generic;

namespace WebApplication1.EfStuff.Model
{
    public class Election : BaseModel
    {
        public string Name { get; set; }

        public virtual Ballot Ballot { get; set; }
        public virtual ICollection<Ballot> Ballots { get; set; }

        public string Description { get; set; }
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}