using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class Passenger : BaseModel
    {
        public string Name { get; set; }
        public string Age { get; set; }
        /// <summary>
        /// CitizenId is nullable long beacause passenger can be foreigner
        /// </summary>
        public long? CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}
