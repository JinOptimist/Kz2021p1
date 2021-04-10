using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;

namespace WebApplication1.Services.Police
{
	public interface IPoliceService
	{
		List<Citizen> GetAll();
		bool CitizenToJail(long id);
		bool AddedToPoliceAcademy(PoliceAcademy policeAcademy);
		bool AddViolationForUser(Violations violation);
		bool AmnestyAllSeverity(long id);
		bool CreateCall(PoliceCallHistory policeCallHistory);
	}
}
