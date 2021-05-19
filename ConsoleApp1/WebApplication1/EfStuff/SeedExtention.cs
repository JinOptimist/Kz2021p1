using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!flightsRepository.HasAnyFlights())
            {
                Random random = new Random();
                FlightStatus[] incomingStatuses = new FlightStatus[] { FlightStatus.Expected, FlightStatus.Delayed, FlightStatus.Landed };
                FlightStatus[] departingStatuses = new FlightStatus[] { FlightStatus.Canceled, FlightStatus.OnTime, FlightStatus.Departed, FlightStatus.Canceled };
                string[] places = new string[] { "Moscow", "New York", "Sydney", "Los Angeles", "Berlin", "Tokyo", "Paris", "Istanbul", "Rome", "Krakow", "Singapore" };
                string[] airlines = new string[] { "International Airline", "Southwest Airline", "Delta Airline", "United Airline", "UC Airline", "Rex Airline" };
                for (int i = 0; i < random.Next(10, 50); i++)
                {
                    Flight departingFlight = new Flight()
                    {
                        TailNumber = GenerateTailNumber(random),
                        FlightType = FlightType.DepartingFlight,
                        Airline = airlines[random.Next(airlines.Length)],
                        FlightStatus = departingStatuses[random.Next(departingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(12)).AddMinutes(random.Next(30))
                    };
                    flightsRepository.Save(departingFlight);
                }
                for (int i = 0; i < random.Next(10, 50); i++)
                {
                    Flight incomingFlight = new Flight()
                    {
                        TailNumber = GenerateTailNumber(random),
                        FlightType = FlightType.IncomingFlight,
                        Airline = airlines[random.Next(airlines.Length)],
                        FlightStatus = incomingStatuses[random.Next(incomingStatuses.Length)],
                        Place = places[random.Next(places.Length)],
                        Date = DateTime.Now.AddHours(random.Next(12)).AddMinutes(random.Next(30))
                    };
                    flightsRepository.Save(incomingFlight);
                }
            }
        }

        private static string GenerateTailNumber(Random random)
        {
            var firstPart = $"{(char)('A' + random.Next(26))}{(char)('A' + random.Next(26))}";
            var secondPart = $"{(char)('0' + random.Next(9))}{(char)('0' + random.Next(9))}";
            return $"{firstPart} {secondPart}";
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
