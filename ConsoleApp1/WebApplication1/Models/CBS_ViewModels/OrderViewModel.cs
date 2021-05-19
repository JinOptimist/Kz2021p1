using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string RouteTitle { get; set; }
        public double Period { get; set; }
        public double FinalPrice { get; set; }
    }
}