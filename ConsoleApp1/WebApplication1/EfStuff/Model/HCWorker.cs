using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class HCWorker : BaseModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Contacts { get; set; }
        public string Password { get; set; }
        public virtual Citizen Citizen { get; set; }
        public virtual HCEstablishments Facility { get; set; }
        public long CitizenId { get; set; }
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
    }
}
