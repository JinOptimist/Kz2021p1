using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
	public class PoliceAcademyRequestVM
    {
		[Required(ErrorMessage = "Please enter your first name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter your last name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter your birth day")]
		public DateTime BirthDate { get; set; }

		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[Required(ErrorMessage = "Please enter yot email")]
		public string EMail { get; set; }

		[Required(ErrorMessage = "Please enter your phone number")]
		[StringLength(25)]
		[DataType(DataType.PhoneNumber)]
		[Phone]
		public string PhoneNumber { get; set; }
	}
}
