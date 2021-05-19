using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Services;

namespace WebApplication1.EfStuff
{
    public static class SeedExtention
    {
        public const string AdminName = "admin";
        public const string FacilityName = "Mediker";

        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultCitizen(scope.ServiceProvider);



                CreateDefaultHCEstablishments(scope.ServiceProvider);

                CreateDefaultHCWorker(scope.ServiceProvider);

                CreateDefaultBusPark(scope.ServiceProvider);
                //CreateDefaultRoutes(scope.ServiceProvider);
            }

            return host;
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

                Console.WriteLine(admin.Id);
                citizenRepository.Save(admin);
            }
        }

        private static void CreateDefaultHCEstablishments(IServiceProvider serviceProvider)
        {
            var establishmentsRepository = serviceProvider.GetService<IHCEstablishmentsRepository>();

            var facility = establishmentsRepository.GetByName(FacilityName);

            if (facility == null)
            {
                facility = new HCEstablishments()
                {
                    Name = FacilityName,
                    Webpage = "mediker.com",
                    Contacts = 0001,
                    Address = "satbayeva"
                };

                Console.WriteLine(facility.Id);
                establishmentsRepository.Save(facility);
            }
        }

        private static void CreateDefaultHCWorker(IServiceProvider serviceProvider)
        {
            var hcworkerRepository = serviceProvider.GetService<IHCWorkerRepository>();
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();
            var establishmentsRepository = serviceProvider.GetService<IHCEstablishmentsRepository>();

            var citizenId = citizenRepository.GetByName(AdminName).Id;
            var facilityId = establishmentsRepository.GetByName(FacilityName).Id;

            var hcadmin = hcworkerRepository.GetByName(AdminName);

            if (hcadmin == null)
            {
                hcadmin = new HCWorker()
                {
                    FacilityId = facilityId,
                    CitizenId = citizenId,
                    Name = AdminName,
                    Position = "big boss",
                    Contacts = 0000,
                };

                hcworkerRepository.Save(hcadmin);
            }
        }


        public const string busTypeOrdinary = "ordinary";
        public const string busTypeTouristic = "touristic";
        public const string busTypeSchool = "school";
        public const string busTypeCity = "city";
        public const string busTypeSpecial = "special";
        public const string busTypeCityAdministration = "administration";
        private static void CreateDefaultBusPark(IServiceProvider serviceProvider)
        {
            var busRepository = serviceProvider.GetService<IBusRepository>();
            var tripRepository = serviceProvider.GetService<ITripRouteRepository>();

            var busOrdinary = busRepository.GetByType(busTypeOrdinary);
            var busTouristic = busRepository.GetByType(busTypeTouristic);
            var busSchool = busRepository.GetByType(busTypeOrdinary);
            var busCity = busRepository.GetByType(busTypeOrdinary);
            var busSpecial = busRepository.GetByType(busTypeOrdinary);
            var busCityAdministration = busRepository.GetByType(busTypeOrdinary);

            if (busOrdinary == null)
            {
                busOrdinary = new Bus()
                {
                    Model = "IKAR",
                    Capacity = 30,
                    Type = "ordinary",
                    Price = 50,
                    RoutePlan = new TripRoute()
                    {

                        Title = "Daily",
                        Type = "ordinary",
                        Length = 1,
                        TripTime = 1,
                        Price = 1,
                        Buses = null
                    }
                };

                busRepository.Save(busOrdinary);
            }

            if (busTouristic == null)
            {
                busTouristic = new Bus()
                {
                    Model = "Aurora",
                    Capacity = 50,
                    Type = "touristic",
                    Price = 80,
                    RoutePlan = new TripRoute()
                    {

                        Title = "GrandTour",
                        Type = "touristic",
                        Length = 1,
                        TripTime = 1,
                        Price = 1,
                        Buses = null
                    }
                };

                busRepository.Save(busTouristic);
            }

            if (busSchool == null)
            {
                busSchool = new Bus()
                {
                    Model = "STAR",
                    Capacity = 60,
                    Type = "school",
                    Price = 90,
                    RoutePlan = new TripRoute()
                    {

                        Title = "Special",
                        Type = "special",
                        Length = 1,
                        TripTime = 1,
                        Price = 1,
                        Buses = null
                    }
                };

                busRepository.Save(busSchool);
            }




        }


        //public const string routeTypeOrdinary = "ordinary";
        //public const string routeTypeTouristic = "touristic";
        //public const string routeTypeSpecial = "special";

        //private static void CreateDefaultRoutes(IServiceProvider serviceProvider)
        //{
        //    var tripRouteRepository = serviceProvider.GetService<ITripRouteRepository>();
        //    var busRepository = serviceProvider.GetService<IBusRepository>();

        //    var routeOrdinary = tripRouteRepository.GetByType(routeTypeOrdinary);
        //    var routeTouristic = tripRouteRepository.GetByType(routeTypeTouristic);            
        //    var routeSpecial = tripRouteRepository.GetByType(routeTypeSpecial);


        //    if (routeOrdinary == null)
        //    {

        //        routeOrdinary = new TripRoute()
        //        {
        //            Title = "Daily",
        //            Type = "ordinary",
        //            Length = 50,
        //            Price = 5,
        //            TripTime = 10,
        //            Buses = busRepository.GetAll().Where(x => x.Type == "ordinary").ToList()
        //        };

        //        tripRouteRepository.Save(routeOrdinary);
        //    }

        //    if (routeTouristic == null)
        //    {
        //        routeTouristic = new TripRoute()
        //        {
        //            Title = "GrandTour",
        //            Type = "touristic",
        //            Length = 50,
        //            Price = 5,
        //            TripTime = 10,
        //            Buses = busRepository.GetAll().Where(x => x.Type == "touristic").ToList()
        //        };

        //        tripRouteRepository.Save(routeTouristic);
        //    }

        //    if (routeSpecial == null)
        //    {
        //        routeSpecial = new TripRoute()
        //        {
        //            Title = "Secret",
        //            Type = "special",
        //            Length = 50,
        //            Price = 5,
        //            TripTime = 10,
        //            Buses = busRepository.GetAll().Where(x => x.Type == "special").ToList()
        //        };

        //        tripRouteRepository.Save(routeSpecial);
        //    }


        //}

    }
}