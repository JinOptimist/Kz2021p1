using AutoMapper;
using System;
using WebApplication1.EfStuff.Model;
using WebApplication1.ViewModels;

namespace WebApplication1.Profiles
{
	public class PoliceProfiles : Profile
    {
		public PoliceProfiles()
		{
			CreateMap<PoliceAcademyRequestVM, PoliceAcademy>();
			CreateMap<ViolationViewModel, Violations>();
			CreateMap<PoliceCallViewModel, PoliceCallHistory>();
		}
    }
}
