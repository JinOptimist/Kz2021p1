using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.Models.Airport;

namespace WebApplication1.Profiles.Airport
{
    public class AirportProfiles : Profile
    {
        public AirportProfiles()
        {
            CreateMap<Flight, IncomingFlightInfoViewModel>()
                .ForMember(nameof(IncomingFlightInfoViewModel.ETA), opt => opt.MapFrom(f => f.Date.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(nameof(IncomingFlightInfoViewModel.Origin), opt => opt.MapFrom(f => f.Place));
            CreateMap<IncomingFlightInfoViewModel, Flight>()
                .ForMember(nameof(Flight.Date), opt => opt.MapFrom(viewModel => DateTime.Parse(viewModel.ETA)))
                .ForMember(nameof(Flight.Place), opt => opt.MapFrom(viewModel => viewModel.Origin));
            CreateMap<Flight, DepartingFlightInfoViewModel>()
                .ForMember(nameof(DepartingFlightInfoViewModel.DepartureTime), opt => opt.MapFrom(f => f.Date.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(nameof(DepartingFlightInfoViewModel.Destination), opt => opt.MapFrom(f => f.Place));
            CreateMap<DepartingFlightInfoViewModel, Flight>()
                .ForMember(nameof(Flight.Date), opt => opt.MapFrom(viewModel => DateTime.Parse(viewModel.DepartureTime)))
                .ForMember(nameof(Flight.Place), opt => opt.MapFrom(viewModel => viewModel.Destination));
            CreateMap<Flight, AvailableFlightsViewModel>()
                .ForMember(nameof(AvailableFlightsViewModel.DepartureTime), opt => opt.MapFrom(f => f.Date.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(nameof(DepartingFlightInfoViewModel.Destination), opt => opt.MapFrom(f => f.Place));
            CreateMap<AvailableFlightsViewModel, Flight>()
                .ForMember(nameof(Flight.Date), opt => opt.MapFrom(viewModel => DateTime.Parse(viewModel.DepartureTime)))
                .ForMember(nameof(Flight.Place), opt => opt.MapFrom(viewModel => viewModel.Destination));
            CreateMap<Citizen, Passenger>()
                .ForMember(nameof(Passenger.CitizenId), opt => opt.MapFrom(c => c.Id))
                .ForMember(nameof(Passenger.Id), opt => opt.Ignore());
        }
    }
}
