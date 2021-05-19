using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public class FireTruck : BaseModel
    {
        public string TruckNumber { get; set; }
        public string TruckState { get; set; }
        public virtual FiremanTeam FiremanTeam { get; set; }
    }
}
