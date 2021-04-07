using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("Policemen", Schema = "Police")]
	public class Policeman : BaseModel
    {
		[Required]
		public Rank Rank { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,4)")]
		public decimal Salary { get; set; }

		[Required]
		public DateTime StartWork { get; set; }

		[Required]
		[ForeignKey("Citizen")]
		public long CitizenId { get; set; }

		[Required]
		public virtual Citizen Citizen { get; set; }

		[JsonIgnore]
		public virtual ICollection<Violations> Violations { get; set; }
		[JsonIgnore]
		public virtual ICollection<PoliceCallHistory> PoliceCallHistories { get; set; }
	}
}
