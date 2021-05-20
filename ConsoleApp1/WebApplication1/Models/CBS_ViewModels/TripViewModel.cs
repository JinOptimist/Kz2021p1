using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class TripViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Length { get; set; }
        public decimal Price { get; set; }
        public List<Bus> Buses { get; set; }
    }
}