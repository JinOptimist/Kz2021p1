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
                var arrivingFlights = _flightsRepository.GetArrivingFlights();
                arrivingFlights.ForEach(f =>
                {
                    f.Passengers.ForEach(p =>
                    {
                        p.Flights.Remove(f);
                        p.Citizen.IsOutOfCity = false;
                        _citizenRepository.Save(p.Citizen);
                    });
                });
                ConvertFlights(arrivingFlights, _flightsRepository);
                Debug.WriteLine("Admited");
            }
        }

        private void DepartPassengers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _citizenRepository = scope.ServiceProvider.GetRequiredService<ICitizenRepository>();
                var _flightsRepository = scope.ServiceProvider.GetRequiredService<IFlightsRepository>();
                var departingFlights = _flightsRepository.GetDepartingFlights();
                departingFlights.ForEach(f =>
                {
                    f.Passengers.ForEach(p =>
                    {
                        p.Citizen.IsOutOfCity = true;
                        _citizenRepository.Save(p.Citizen);
                    });
                });
                ConvertFlights(departingFlights, _flightsRepository);
                Debug.WriteLine("Departed");
            }
        }

        private void ConvertFlights(List<Flight> flights, IFlightsRepository _flightsRepository)
        {
            Random random = new Random();
            string[] places = new string[] { "Moscow", "New York", "Sydney", "Los Angeles", "Berlin", "Tokyo", "Paris", "Istanbul", "Rome", "Krakow", "Singapore" };
            foreach (var flight in flights)
            {
                flight.Place = places[random.Next(places.Length)];
                if (flight.FlightType == FlightType.IncomingFlight)
                {
                    flight.FlightType = FlightType.DepartingFlight;
                    flight.FlightStatus = FlightStatus.OnTime;
                }
                else
                {
                    flight.FlightType = FlightType.IncomingFlight;
                    flight.FlightStatus = FlightStatus.Expected;
                }
                flight.Date = DateTime.Now.AddDays(random.Next(5));
                _flightsRepository.Save(flight);
            }
        }
    }
}
