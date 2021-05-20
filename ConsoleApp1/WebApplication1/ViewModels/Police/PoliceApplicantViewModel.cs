using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
	public class PoliceApplicantViewModel
    {
		public long	Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime BirthDate { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

	}
}
