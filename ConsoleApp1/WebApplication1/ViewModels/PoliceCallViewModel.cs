using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.ViewModels
{
	public class PoliceCallViewModel
    {
		[Required(ErrorMessage = "Please write decription about situation")]
		public string Description { get; set; }
		[Required]
		public CallPriority CallPriority { get; set; }
	}
}
