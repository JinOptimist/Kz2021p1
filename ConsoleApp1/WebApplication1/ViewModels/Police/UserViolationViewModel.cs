using System;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.ViewModels
{
	public class UserViolationViewModel
    {
		public long ViolationId { get; set; }
		public SeverityViolation SeverityViolation { get; set; }
		public string Description { get; set; }
		public DateTime DateExpired { get; set; }
	}
}
