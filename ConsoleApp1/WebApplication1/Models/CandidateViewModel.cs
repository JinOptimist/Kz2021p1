using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class CandidateViewModel
    {
        public long Id { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required]
        [Range(18, 120)]  
        public int Age { get; set; }

        [Required] 
        public string Job { get; set; }

        public string Slogan { get; set; }

        public City City { get; set; }

        public Idea Idea { get; set; }

        public Citizen Citizen { get; set; }

        public Election Election { get; set; }

        public List<Ballot> Ballots { get; set; }
    }
}