using System.Collections.Generic;

namespace WebApplication1.ViewModels
{
	public class QuestionAndAnswer
    {
		public long QuestionId { get; set; }
		public string QuestionDesc { get; set; }
		public List<AnswerViewModel> Answers { get; set; }
	}
}
