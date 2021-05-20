using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("Shifts", Schema = "Police")]
	public class PoliceShift : BaseModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public long PolicemanId { get; set; }
		public virtual Policeman Policeman { get; set; }
	}
}
