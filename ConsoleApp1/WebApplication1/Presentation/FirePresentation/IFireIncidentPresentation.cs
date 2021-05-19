using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Presentation.FirePresentation
{
	public interface IFireIncidentPresentation
	{
		void AddFireIncident(FireIncidentViewModel model);
		void Edit(FireIncidentViewModel model);
		List<FireIncidentViewModel> GetAllIncidents();
		FireIncidentViewModel GetIncident(long id);
		bool Remove(long id);
	}
}
