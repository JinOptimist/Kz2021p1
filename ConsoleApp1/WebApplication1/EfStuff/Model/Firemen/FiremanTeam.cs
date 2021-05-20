using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public class FiremanTeam : BaseModel
    {
        [Required]
        public string TeamName { get; set; }
        [Required]
        public WorkShift Shift { get; set; }
        [ForeignKey("FireTruck")]
        public long? TruckId { get; set; }
        [Required]
        public TeamState TeamState { get; set; }
        public virtual FireTruck FireTruck { get; set; }
        public virtual List<Fireman> Firemen { get; set; }
        public virtual List<FireIncident> FireIncidents { get; set; }

    }
}
