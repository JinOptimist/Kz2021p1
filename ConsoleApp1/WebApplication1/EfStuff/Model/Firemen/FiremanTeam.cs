using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public class FiremanTeam : BaseModel
    {
        public string TeamName { get; set; }
        [ForeignKey("FireTruck")]
        public long TruckId { get; set; }
        public string Shift { get; set; }
        public string TeamState { get; set; }
        public virtual FireTruck FireTruck { get; set; }
        public virtual List<Fireman> Firemen { get; set; }
        public virtual List<FireIncident> FireIncidents { get; set; }

    }
}
