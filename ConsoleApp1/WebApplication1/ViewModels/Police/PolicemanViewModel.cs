using WebApplication1.EfStuff.Model;

namespace WebApplication1.ViewModels
{
	public class PolicemanViewModel
    {
		public long Id { get; set; }
		public double Salary { get; set; }
		public Rank Rank { get; set; }
		public string Name { get; set; }
	}
}
