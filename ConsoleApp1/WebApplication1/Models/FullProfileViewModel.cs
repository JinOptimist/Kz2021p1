using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class FullProfileViewModel
    {       
        public string Name { get; set; }

        public int Age { get; set; }
        
        public string Job { get; set; }

        public DateTime RegistrationDate { get; set; }

        public CompanyViewModel Company { get; set; }
    }
}
