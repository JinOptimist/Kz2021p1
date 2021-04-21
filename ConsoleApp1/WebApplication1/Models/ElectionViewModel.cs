using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class ElectionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Ballot> VotingBox { get; set; }
    }
}
