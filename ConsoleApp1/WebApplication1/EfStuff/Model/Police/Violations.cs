using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.EfStuff.Model
{
	[Table("Violations", Schema = "Police")]
	public class Violations : BaseModel
	{
		public SeverityViolation SeverityViolation { get; set; }

		[Required(ErrorMessage = "Set date when violatiobs will expired")]
		public DateTime DateExpired { get; set; }

		[Required(ErrorMessage = "Please write description about violation")]
		[StringLength(400, ErrorMessage = "Maximun length is 400 symbols")]
		public string Description { get; set; }

		public long CitizenId { get; set; }
		[JsonIgnore]
		public virtual Citizen Citizen { get; set; }

		public long PolicemanId {get; set;}
		[JsonIgnore]
		public virtual Policeman Policeman { get; set; }

	}
}
