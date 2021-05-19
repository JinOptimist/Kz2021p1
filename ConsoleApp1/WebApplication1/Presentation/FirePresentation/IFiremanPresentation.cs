﻿using System.Collections.Generic;
using WebApplication1.Models;

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
	}
}