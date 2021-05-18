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

                CreateDefaultAdress(scope.ServiceProvider);

                CreateDefaultHCEstablishments(scope.ServiceProvider);

                CreateDefaultHCWorker(scope.ServiceProvider);
            }

            return host;
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
    }
}
