using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Firemen;

namespace WebApplication1.Models.FiremanModels
{
    public class FiremanTeamViewModel
    {
        public long Id { get; set; }
        public string TeamName { get; set; }
        public long TruckId { get; set; }
        public WorkShift Shift { get; set; }
        public TeamState TeamState { get; set; }
        public TruckState TruckState { get; set; }
        public int FiremanCount { get; set; }
        public List<string> FiremenNames { get; set; }
    }
}
