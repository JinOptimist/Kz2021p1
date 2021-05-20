using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.EfStuff;

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

        public List<CandidateViewModel> Candidates { get; set; }

    }
}