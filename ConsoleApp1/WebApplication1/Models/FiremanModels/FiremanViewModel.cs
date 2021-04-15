using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class FiremanViewModel
    {       
        public long Id { get; set; }
        public string Role { get; set; }
        public int WorkExperYears { get; set; }
        public string Name { get; set; }     
        
  
    }
}
