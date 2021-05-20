using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
			CreateMap<Violations, UserViolationViewModel>()
				.ForMember(v => v.ViolationId, opt => opt.MapFrom(x => x.Id));
			CreateMap<Citizen, UserInfoViewModel>()
				.ForMember(p => p.Street, opt => opt.MapFrom(x => x.House.Street))
				.ForMember(p => p.HouseNumber, opt => opt.MapFrom(x => x.House.HouseNumber))
				.ForMember(c => c.CitizenId, opt => opt.MapFrom(x => x.Id));
			CreateMap<Policeman, PolicemanViewModel>()
				.ForMember(x => x.Name, opt => opt.MapFrom(x => x.Citizen.Name));
			CreateMap<PoliceAcademy, PoliceApplicantViewModel>();
			CreateMap<PoliceQuizQuestion, QuestionAndAnswer>()
				.ForMember(x => x.QuestionDesc, opt => opt.MapFrom(x => x.Description))
				.ForMember(x => x.QuestionId, opt => opt.MapFrom(y => y.Id));
			CreateMap<PoliceQuizAnswer, PoliceAnswerViewModel>()
				.ForMember(a => a.AnswerId, opt => opt.MapFrom(x => x.Id));
			CreateMap<BasePolicemanShiftVM, PoliceShift>()
				.ReverseMap();
			CreateMap<PoliceShift, SheriffShiftVM>()
				.ForMember(sf => sf.Name, opt => opt.MapFrom(x => x.Policeman.Citizen.Name));
		}
	}
}
