using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public class FireIncident : BaseModel
    {
        [Required]
        public string Address { get; set; }
        public IncidentStatus Status {get; set;}
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public int? Injured { get; set; }
        public int? Dead { get; set; }
        [ForeignKey("FiremanTeam")]
        public long? TeamId { get; set; }
        public virtual FiremanTeam FiremanTeam { get; set; }
    }
}
