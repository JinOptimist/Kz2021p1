using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("Question", Schema = "Police")]
	public class PoliceQuizQuestion : BaseModel
    {
		public string Description { get; set; }
		public virtual ICollection<PoliceQuizAnswer> Answers { get; set; }
	}
}
