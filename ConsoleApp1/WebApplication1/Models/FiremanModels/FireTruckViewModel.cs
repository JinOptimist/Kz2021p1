using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.FiremanModels
{
    public class FireTruckViewModel
    {
        public long Id { get; set; }
        public string TruckNumber { get; set; }
        public string TeamName { get; set; }
        public string TruckState { get; set; }

    }
}
