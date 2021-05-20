using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Presentation.FirePresentation
{
	public interface IFiremanPresentation
	{
		void CreateFireman(FiremanViewModel model);
		string Edit(FiremanViewModel model);
		List<FiremanViewModel> GetAllFiremen();
		FiremanViewModel GetFireman(long id);
		FiremanViewModel MyProfile();
		bool Remove(long id);
		List<FireIncidentViewModel> GetCurrentIncidents();
		
	}
}
