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
        
        public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();
        
    }
}
