using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class Passenger : BaseModel
    {
        [ForeignKey("Flight")]
        public long FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        [ForeignKey("Citizen")]
        public long CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
    }
}
