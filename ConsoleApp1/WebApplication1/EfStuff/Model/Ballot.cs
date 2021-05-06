using System;
using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff
{
    public class Ballot : BaseModel
    {
        public virtual Election Election { get; set; }

        public virtual ICollection<Election> Elections { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<Citizen> Citizens { get; set; }

        public DateTime VoteTime { get; set; }
    }
}