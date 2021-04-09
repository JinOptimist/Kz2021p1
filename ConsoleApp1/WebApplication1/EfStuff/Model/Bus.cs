using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Bus : BaseModel
    {
        public string Model { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public virtual TripRoute RoutePlan { get; set; }
    }
}
