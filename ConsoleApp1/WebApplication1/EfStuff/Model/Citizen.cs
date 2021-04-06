using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Citizen : BaseModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public DateTime CreatingDate { get; set; }

        public virtual Adress House { get; set; }
    }
}
