using System.Collections.Generic;
using System.Security.Claims;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
	public interface ICitizenPresentation
	{
		FullProfileViewModel FullProfile();
		ClaimsPrincipal GetClaimsPrincipal(Citizen user);
		List<FullProfileViewModel> GetIndexViewModel();
		bool Remove(string name);
		void Save(LoginViewModel viewModel);
	}
}