using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
	public class PoliceAnswerViewModel
    {
		public long AnswerId { get; set; }

		[Required(ErrorMessage = "Write description about question")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please select correct answer")]
		public bool IsRight { get; set; }
	}
}
