using System;

namespace WebApplication1.ViewModels
{
    public class UserInfoViewModel
    {
		public long CitizenId { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public DateTime CreateDate { get; set; }
		public int HouseNumber { get; set; }
		public string Street { get; set; }
		public string Uri { get; set; }
	}
}
