using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class TripRoute : BaseModel
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public double Length { get; set; }
        public decimal Price { get; set; }
        public double TripTime { get; set; }

        public virtual List<Bus> Buses { get; set; }
    }
}
