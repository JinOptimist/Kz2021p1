using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Model
{
    public class Citizen : BaseModel
    {
        public string Name { get; set; }

		public string AvatarUrl { get; set; }

		public string Password { get; set; }

        public int Age { get; set; }

        public DateTime CreatingDate { get; set; }

		public Local Local { get; set; }

		public virtual Adress House { get; set; } 
        public virtual Fireman Fireman { get; set; }
        public bool IsOutOfCity { get; set; }
		public virtual Policeman Policeman { get; set; }
		public virtual PoliceAcademy PoliceAcademy { get; set; }
		public virtual ICollection<Violations> Violations  {get; set; }
		public virtual ICollection<PoliceCallHistory> PoliceCallHistories { get; set; }
        public virtual Passenger Passenger { get; set; }
		public virtual HCWorker HCWorker { get; set; }
	}
}

