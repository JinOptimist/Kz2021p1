using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.FiremanRepo;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.EfStuff.Repositoryies.Interface.FiremanInterface;

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
                CreateDefaultFireAdmin(scope.ServiceProvider);
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
                citizenRepository.Save(admin);
            }
        }
        private static void CreateDefaultFireAdmin(IServiceProvider serviceProvider)
        {
            var firemanRepository = serviceProvider.GetService<IFiremanRepository>();
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();

            var firecitizen = citizenRepository.GetByName("FireAdmin");
            if (firecitizen == null)
            {
                firecitizen = new Citizen()
                {
                    Name = "FireAdmin",
                    Password = "fireadmin",
                    Age = 40
                };
                citizenRepository.Save(firecitizen);
                var fireadmin = new Fireman()
                {
                    Role = FireWorkerRole.Fireadmin,
                    WorkExperYears = 10,
                    CitizenId = firecitizen.Id,
                    Citizen = firecitizen
                };
                firemanRepository.Save(fireadmin);
            }
        }
    }
}
