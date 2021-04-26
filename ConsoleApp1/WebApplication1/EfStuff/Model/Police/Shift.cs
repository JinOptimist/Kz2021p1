using System;

namespace WebApplication1.EfStuff.Model
{
	public class Shift : BaseModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public long PolicemanId { get; set; }
		public virtual Policeman Policeman { get; set; }
	}
}
