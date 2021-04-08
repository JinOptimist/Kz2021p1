using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Adress : BaseModel
    {       
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FloorCount { get; set; }

        public virtual List<Citizen> Citizens { get; set; }
    }
}
