using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.FiremanModels
{
    public class FiremanTeamViewModel
    {
        public long Id { get; set; }
        public string TeamName { get; set; }
        public long TruckId { get; set; }
        public string Shift { get; set; }
        public string TeamState { get; set; }
        public string TruckState { get; set; }
        public int FiremanCount { get; set; }
        public List<string> FiremenNames { get; set; }
    }
}
