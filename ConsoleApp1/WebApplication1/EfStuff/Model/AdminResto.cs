using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.EfStuff.Model
{
	public class AdminResto : BaseModel
	{
		public string LoginAdmin { get; set; }
		public string PasswordAdmin { get; set; }
		public long CitizenId { get; set; }
		public virtual Citizen Citizen { get; set; }
		public virtual Restorans Restoran { get; set; }
	}
}

