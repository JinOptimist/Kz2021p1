using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Presentation.FirePresentation
{
	public interface IFiremanTeamPresentation
	{
		void CreateFiremanTeam(FiremanTeamViewModel model);
		void Edit(FiremanTeamViewModel model);
		List<FiremanTeamViewModel> GetAllTeams();
		FiremanTeamViewModel GetTeam(long id);
		bool Remove(long id);
	}
}
