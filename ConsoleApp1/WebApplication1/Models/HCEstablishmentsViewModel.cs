using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class HCEstablishmentsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Contacts { get; set; }
        public string Webpage { get; set; }
        public virtual List<HCWorker> Worker {get; set;}
    }
}
