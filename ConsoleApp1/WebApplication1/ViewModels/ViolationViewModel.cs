using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.ViewModels
{
	public class ViolationViewModel
    {
		[Required(ErrorMessage = "Selected severity of vioalation")]
		public SeverityViolation SeverityViolation { get; set; }

		[Required(ErrorMessage = "Set date when violations will expired")]
		public DateTime DateExpired { get; set; }

		[Required(ErrorMessage = "Please write description about violation")]
		[StringLength(400, ErrorMessage = "Maximun length is 400 symbols")]
		public string Description { get; set; }

		public long CitizenId { get; set; }
	}
}
