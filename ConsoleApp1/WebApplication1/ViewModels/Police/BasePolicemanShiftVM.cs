using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
	public class BasePolicemanShiftVM
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "Please choose start date of shift")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Please choose end date of shift")]
		public DateTime EndDate { get; set; }
		public long PolicemanId { get; set; } 
	}
}
