using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff
{
	public static class SeedExtention
    {
        public const string AdminName = "admin";

        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultCitizen(scope.ServiceProvider);

                CreateDefaultAdress(scope.ServiceProvider);
                CreateDefaultFlights(scope.ServiceProvider);
            }

            return host;
        }

        private static void CreateDefaultFlights(IServiceProvider serviceProvider)
        {
            IFlightsRepository flightsRepository = serviceProvider.GetService<IFlightsRepository>();
            if (flightsRepository.GetAll().Count == 0)
            {
                Random random = new Random();
                FlightStatus[] incomingStatuses = new[] { FlightStatus.Expected, FlightStatus.Delayed, FlightStatus.Landed };
                FlightStatus[] departingStatuses = new[] { FlightStatus.Canceled, FlightStatus.OnTime, FlightStatus.Departed, FlightStatus.Canceled };
                string[] places = new[] { "Moscow", "New York", "Sydney", "Los Angeles", "Berlin", "Tokyo", "Paris", "Istanbul", "Rome", "Krakow", "Singapore" };
                for (int i = 0; i < random.Next(5, 10); i++)
                {
                    Flight incomingFlight = new Flight()
                    {
                        TailNumber = $"{(char)('A' + random.Next(26))}{(char)('A' + random.Next(26))} {(char)('0' + random.Next(9))}{(char)('0' + random.Next(9))}",
                        FlightType = FlightType.IncomingFlight,
                        Airline = "International Airline",
                        FlightStatus = incomingStatuses[random.Next(incomingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(1, 10))
                    };
                    flightsRepository.Save(incomingFlight);
                }
                for (int i = 0; i < random.Next(5, 10); i++)
                {
                    Flight departingFlight = new Flight()
                    {
                        TailNumber = $"{(char)('A' + random.Next(26))}{(char)('A' + random.Next(26))} {(char)('0' + random.Next(9))}{(char)('0' + random.Next(9))}",
                        FlightType = FlightType.DepartingFlight,
                        Airline = "International Airline",
                        FlightStatus = departingStatuses[random.Next(departingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(1, 10))
                    };
                    flightsRepository.Save(departingFlight);
                }
            }
        }

        private static void CreateDefaultAdress(IServiceProvider serviceProvider)
        {
        }

        private static void CreateDefaultCitizen(IServiceProvider serviceProvider)
        {
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();

            var admin = citizenRepository.GetByName(AdminName);
            if (admin == null)
            {
                admin = new Citizen()
                {
                    Name = AdminName,
                    Password = "admin",
                    Age = 30
                };
                citizenRepository.Save(admin);
            }
        }
    }
}
