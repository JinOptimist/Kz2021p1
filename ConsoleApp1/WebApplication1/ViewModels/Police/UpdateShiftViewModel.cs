using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.ViewModels
{
	public class UpdateShiftViewModel : BaseModel
    {
		[Required(ErrorMessage = "Please choose start date of shift")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Please choose end date of shift")]
		public DateTime EndDate { get; set; }
	}
}
