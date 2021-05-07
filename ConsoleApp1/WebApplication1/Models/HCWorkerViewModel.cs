using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.Models
{
    public class HCWorkerViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Contacts { get; set; }
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
        public long CitizenId { get; set; }
    }
}
