using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("PoliceCallHistory", Schema = "Police")]
	public class PoliceCallHistory : BaseModel
    {
		[Required]
		public DateTime DateCall { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public CallPriority CallPriority { get; set; }
		[Required]

		public long CitizenId { get; set; }
		public virtual Citizen Citizen { get; set; }

		public long PolicemanId { get; set; }
		public virtual Policeman Policeman { get; set; }
	}
}
