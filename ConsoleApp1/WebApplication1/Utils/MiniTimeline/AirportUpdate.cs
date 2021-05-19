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
            AdmitPassengers();
            Task.WhenAll(
                Task.Run(() => LandFlights()),
                Task.Run(() => DepartPassengers()),
                Task.Run(() => ReturnFlights())
                );
        }

        private void LandFlights()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var landedFlights = 0;
                var _citizenRepository = scope.ServiceProvider.GetRequiredService<ICitizenRepository>();
                var _flightsRepository = scope.ServiceProvider.GetRequiredService<IFlightsRepository>();
                var arrivingFlights = _flightsRepository.GetArrivingFlights();
                arrivingFlights.ForEach(f =>
                {
                    f.FlightStatus = FlightStatus.Landed;
                    _flightsRepository.Save(f);
                    landedFlights++;
                });
            }
        }

        private void AdmitPassengers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var admittedFlights = 0;
                var _citizenRepository = scope.ServiceProvider.GetRequiredService<ICitizenRepository>();
                var _flightsRepository = scope.ServiceProvider.GetRequiredService<IFlightsRepository>();
                var arrivingFlights = _flightsRepository.GetLandedFlights();
                arrivingFlights.ForEach(f =>
                {
                    f.Passengers.ForEach(p =>
                    {
                        p.Flights.Remove(f);
                        p.Citizen.IsOutOfCity = false;
                        _citizenRepository.Save(p.Citizen);
                    });
                    admittedFlights++;
                });
                ConvertFlights(arrivingFlights, _flightsRepository);
            }
        }

        private void DepartPassengers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var departedFlights = 0;
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
                    f.FlightStatus = FlightStatus.Departed;
                    _flightsRepository.Save(f);
                    departedFlights++;
                });
            }
        }

        private void ReturnFlights()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _citizenRepository = scope.ServiceProvider.GetRequiredService<ICitizenRepository>();
                var _flightsRepository = scope.ServiceProvider.GetRequiredService<IFlightsRepository>();
                var returningFlights = _flightsRepository.GetDepartedFlights();
                ConvertFlights(returningFlights, _flightsRepository);
            }
        }

        private void ConvertFlights(List<Flight> flights, IFlightsRepository _flightsRepository)
        {
            Random random = new Random();
            string[] places = new string[] { "Moscow", "New York", "Sydney", "Los Angeles", "Berlin", "Tokyo", "Paris", "Istanbul", "Rome", "Krakow", "Singapore" };
            foreach (var flight in flights)
            {
                if (flight.FlightStatus == FlightStatus.Landed)
                {
                    flight.Place = places[random.Next(places.Length)];
                    flight.FlightType = FlightType.DepartingFlight;
                    flight.FlightStatus = FlightStatus.OnTime;
                }
                else if (flight.FlightStatus == FlightStatus.Departed)
                {
                    flight.FlightType = FlightType.IncomingFlight;
                    flight.FlightStatus = FlightStatus.Expected;
                }
                flight.Date = DateTime.Now.AddDays(random.Next(5)).AddHours(random.Next(12)).AddMinutes(random.Next(30));
                _flightsRepository.Save(flight);
            }
        }
    }
}
