using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class ElectionViewModel
    {
        public bool IsVoted { get; set; }

        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public virtual Candidate Candidate { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        
        public virtual ICollection<Ballot> Ballots { get; set; }
    }
}
