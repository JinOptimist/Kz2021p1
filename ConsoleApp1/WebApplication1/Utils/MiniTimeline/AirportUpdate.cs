using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.Utils.MiniTimeline
{
    public class AirportUpdate
    {
        private IServiceProvider _serviceProvider;
        public AirportUpdate(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AirportStateUpdater()
        {
            Task.WhenAll(
                Task.Run(() => AdmitPassengers()),
                Task.Run(() => DepartPassengers()
                ));
        }

        private void AdmitPassengers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _citizenRepository = scope.ServiceProvider.GetRequiredService<ICitizenRepository>();
                var _flightsRepository = scope.ServiceProvider.GetRequiredService<IFlightsRepository>();
                var _passengersRepository = scope.ServiceProvider.GetRequiredService<IPassengersRepository>();
                var arrivedFlights = _flightsRepository.GetArrivingFlights();
                Debug.WriteLine(arrivedFlights.Count());
                arrivedFlights.ForEach(f =>
                {
                    f.Passengers.ForEach(p =>
                    {
                        p.Flights.Remove(f);
                        p.Citizen.IsOutOfCity = false;
                        _citizenRepository.Save(p.Citizen);
                    });
                });
                Debug.WriteLine("Admited");

            }
        }

        private async Task DepartPassengers()
        {
            //TODO: move DepartPassengers to MiniTimeline
            //List<Flight> departedFlights = new List<Flight>();
            //foreach (var passenger in _passengersRepository.GetAllPassengersAvailableForDeparture())
            //{
            //    if (!departedFlights.Contains(passenger.Flight))
            //    {
            //        departedFlights.Add(passenger.Flight);
            //    }
            //    passenger.Citizen.IsOutOfCity = true;
            //    _citizenRepository.Save(passenger.Citizen);
            //}
            //ConvertFlights(departedFlights);
            Debug.WriteLine("Departed");
        }
    }
}
