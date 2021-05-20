using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class BusParkViewModel
    {
        public long Id { get; set; }

        public string Model { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool IsOnOrder { get; set; } = false;
        public TripRoute RoutePlan { get; set; }
    }
}