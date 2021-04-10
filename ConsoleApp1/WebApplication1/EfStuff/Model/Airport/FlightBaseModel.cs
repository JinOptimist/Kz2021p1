using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public class FlightBaseModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Airline { get; set; }
    }
}
