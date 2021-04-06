using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AdressViewModel
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FloorCount { get; set; }

        public int CitizenCount { get; set; }

        public string UserName { get; set; }
    }
}
