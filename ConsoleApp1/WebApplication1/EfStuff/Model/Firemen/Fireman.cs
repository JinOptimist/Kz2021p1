using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public class Fireman : BaseModel
    {
        [Required]
        public FireWorkerRole Role { get; set; }
        public int WorkExperYears { get; set; }
        [Required]
        public long CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
        public virtual FiremanTeam FiremanTeam { get; set; }
        public virtual List<FireIncident>  FireIncidents{get; set;}
      
    }
}
