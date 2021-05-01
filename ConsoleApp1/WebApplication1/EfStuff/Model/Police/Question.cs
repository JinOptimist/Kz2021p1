using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("Question", Schema = "Police")]
	public class Question : BaseModel
    {
		public string Description { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
	}
}
