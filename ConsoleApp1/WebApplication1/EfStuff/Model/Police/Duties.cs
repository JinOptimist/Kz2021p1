using System;
using System.Collections.Generic;

namespace WebApplication1.EfStuff.Model
{
	public class Duties : BaseModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int CountOfCall { get; set; }
		public StatusOfficer Status { get; set; }
		public bool WithPartner { get; set; }
	}
}
