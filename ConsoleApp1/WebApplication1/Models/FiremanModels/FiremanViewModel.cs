using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;

namespace WebApplication1.Models
{
    public class FiremanViewModel
    {
        public long Id { get; set; }
        public FireWorkerRole Role { get; set; }
        public int WorkExperYears { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string TeamName { get; set; }

    }
}
