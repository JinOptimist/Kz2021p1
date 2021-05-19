using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.FiremanModels;

namespace WebApplication1.Presentation.FirePresentation
{
	public interface IFireTruckPresentation
	{
		void AddFireTruck(FireTruckViewModel model);
		void Edit(FireTruckViewModel model);
		List<FireTruckViewModel> GetAllTrucks();
		FireTruckViewModel GetTruck(long id);
		bool Remove(long id);
	}
}
