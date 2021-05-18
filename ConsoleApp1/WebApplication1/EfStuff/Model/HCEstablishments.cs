using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class HCEstablishments : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Contacts { get; set; }
        public string Webpage { get; set; }
        public virtual List<HCWorker> Workers { get; set; }
    }
}
