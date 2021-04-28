using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class CandidateViewModel
    {       
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
       
        [Range(18, 120)]
        [Required]
        public int Age { get; set; }
        [Required]
        public string Job { get; set; }

        public string Slogan { get; set; }

        public City City { get; set; }

        public Idea Idea { get; set; }

        public DateTime RegistrationDate { get; set; }

        public long CitizenId { get; set; }
        
        public long ElectionId { get; set; }

    }
}
