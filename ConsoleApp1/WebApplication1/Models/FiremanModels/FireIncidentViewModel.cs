using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.FiremanModels
{
    public class FireIncidentViewModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int Injured { get; set; }
        public int Dead { get; set; }
        public string TeamName { get; set; }
    }
}
