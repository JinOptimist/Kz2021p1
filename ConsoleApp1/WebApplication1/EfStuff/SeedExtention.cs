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
                CreateDefaultCitizens(scope.ServiceProvider);
                CreateDefaultFireTrucks(scope.ServiceProvider);
                CreateDefaultFiremanTeams(scope.ServiceProvider);
                CreateDefaultFiremen(scope.ServiceProvider);
                CreateDefaultIncidents(scope.ServiceProvider);
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
        private static void CreateDefaultCitizens(IServiceProvider serviceProvider)
        {
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();


            List<string> names = new List<string>() { "Martin", "Marvin", "Matt", "Maximilian", "Michael", "Miles", "Murray", "Myron", "Nate", "Nathan", "Neil", "Nicholas", "Nicolas", "Norman", "Oliver", "Oscar", "Osric", "Owen", "Patrick", "Paul", "Peleg", "Philip", "Phillipps", "Raymond", "Reginald" };
            List<string> passwords = new List<string>() { "123456", "xzcv", "asdfg", "qwer", "7885", "rcgh", "tgvc", "vkvk", "2536", "juyt", "cghy", "259j", "cfre", "koiu", "cpgk", "kfjo", "elgj", "bojh", "pfub", "sjfk", "llll", "5874", "8533", "vgty", "zzzz" };
            List<int> ages = new List<int>() { 22, 23, 24, 25, 26, 27, 27, 28, 28, 29, 29, 30, 30, 31, 31, 32, 32, 33, 33, 34, 34, 35, 35, 36, 36 };
            var check = citizenRepository.GetByName("Martin");
            if (check == null)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    Citizen citizen = new Citizen()
                    {
                        Name = names[i],
                        Password = passwords[i],
                        Age = ages[i],
                        CreatingDate = DateTime.Now,
                        AvatarUrl = null

                    };

                    citizenRepository.Save(citizen);
                }
            }

        }
        private static void CreateDefaultFireTrucks(IServiceProvider serviceProvider)
        {
            var fireTruckRepository = serviceProvider.GetService<IFireTruckRepository>();

            var trucks = fireTruckRepository.GetAll().Any();
            List<string> trucknumbers = new List<string>() { "weq456", "dfg789", "yui459", "udh582", "sjf884", "plm872", "asf726", "wsv111" };


            if (!trucks)
            {                
                for (int i = 0; i < trucknumbers.Count - 2; i++)
                {
                    FireTruck truck = new FireTruck()
                    {
                        TruckNumber = trucknumbers[i],
                        TruckState = TruckState.Working
                    };

                    fireTruckRepository.Save(truck);
                }
                FireTruck truckRepairing1 = new FireTruck()
                {
                    TruckNumber = trucknumbers[trucknumbers.Count - 2],
                    TruckState = TruckState.Repairing
                };
                FireTruck truckRepairing2 = new FireTruck()
                {
                    TruckNumber = trucknumbers[trucknumbers.Count - 1],
                    TruckState = TruckState.Repairing
                };
                fireTruckRepository.Save(truckRepairing1);
                fireTruckRepository.Save(truckRepairing2);
            }
        }
        private static void CreateDefaultFiremanTeams(IServiceProvider serviceProvider)
        {
            var firemanTeamRepository = serviceProvider.GetService<IFiremanTeamRepository>();
            var fireTruckRepository = serviceProvider.GetService<IFireTruckRepository>();
            var teams = firemanTeamRepository.GetAll().Any();
            List<string> trucknumbers = new List<string>() { "weq456", "dfg789", "yui459", "udh582", "sjf884", "plm872", "asf726", "wsv111" };
            List<string> teamnames = new List<string>() { "UtopiaHeros", "Titanium", "FireUtopia", "WaterTeam", "HopeTeam", "PowerTeam" };
            List<TeamState> states = new List<TeamState>() { TeamState.Free, TeamState.Free, TeamState.NotWorking, TeamState.NotWorking, TeamState.NotWorking, TeamState.NotWorking };
            List<WorkShift> shifts = new List<WorkShift>() { WorkShift.Day, WorkShift.Day, WorkShift.Morning, WorkShift.Morning, WorkShift.Night, WorkShift.Night };


            if (!teams)
            {
                for (int i = 0; i < teamnames.Count; i++)
                {
                    var truck = fireTruckRepository.GetByNumber(trucknumbers[i]);
                    FiremanTeam team = new FiremanTeam()
                    {
                        TeamName = teamnames[i],
                        TeamState = states[i],
                        Shift = shifts[i],
                        FireTruck = truck,
                        TruckId = truck.Id
                    };
                    truck.FiremanTeam = team;
                    fireTruckRepository.Save(truck);
                    firemanTeamRepository.Save(team);

                }
            }
        }
        private static void CreateDefaultFiremen(IServiceProvider serviceProvider)
        {
            var firemanTeamRepository = serviceProvider.GetService<IFiremanTeamRepository>();
            var firemanRepository = serviceProvider.GetService<IFiremanRepository>();
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();

            var firemen = firemanRepository.GetAll().Any();
            List<string> citizens = new List<string>() { "Martin", "Marvin", "Matt", "Maximilian", "Michael", "Miles", "Murray", "Myron", "Nate", "Nathan", "Neil", "Nicholas", "Nicolas", "Norman", "Oliver", "Oscar", "Osric", "Owen", "Patrick", "Paul", "Peleg", "Philip", "Phillipps", "Raymond", "Reginald" };
            List<string> teamnames = new List<string>() { "UtopiaHeros", "Titanium", "FireUtopia", "WaterTeam", "HopeTeam", "PowerTeam" };
            List<int> wey = new List<int>() { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 2 };


            if (!firemen)
            {
                for (int i = 0; i < citizens.Count - 1; i++)
                {
                    var citizen = citizenRepository.GetByName(citizens[i]);
                    var team = firemanTeamRepository.GetByName(teamnames[i % 6]);
                    Fireman fireman = new Fireman()
                    {
                        Role = FireWorkerRole.FiremanWorker,
                        WorkExperYears = wey[i],
                        Citizen = citizen,
                        CitizenId = citizen.Id,
                        FiremanTeam = team
                    };
                    firemanRepository.Save(fireman);
                }
                var citizen1 = citizenRepository.GetByName(citizens[citizens.Count - 1]);                
                Fireman firemanTruckSpecialist = new Fireman()
                {
                    Role = FireWorkerRole.FireTruckSpecialist,
                    WorkExperYears = wey[citizens.Count - 1],
                    Citizen = citizen1,
                    CitizenId = citizen1.Id,
                };
                firemanRepository.Save(firemanTruckSpecialist);
            }
        }
        private static void CreateDefaultIncidents(IServiceProvider serviceProvider)
        {
            var firemanTeamRepository = serviceProvider.GetService<IFiremanTeamRepository>();
            var fireIncidentRepository = serviceProvider.GetService<IFireIncidentRepository>();

            var incidents = fireIncidentRepository.GetAll().Any();
            List<string> teamnames = new List<string>() { "UtopiaHeros", "Titanium", "FireUtopia", "WaterTeam", "HopeTeam", "PowerTeam" };
            List<string> addresses = new List<string>() { "39455 Paige Estate Apt. 223", "1549 Pfannerstill Union Suite 107", "5889 Rashad Haven Suite 443", "14936 Effie Cape", "84940 Darion Canyon Apt. 245", "1209 Mills Forges Suite 630", "5526 Elwin Cliff", "426 Kohler Pass", "563 Lowe Summit", "8277 Lebsack Alley Apt. 692" };
            List<string> reasons = new List<string>() { "Gas", "Smoking in bedrooms", "Curious children", "Heating", "Electrical equipment", "Faulty wiring", "Barbeques", "Flammable liquids", "Lighting", "Cooking equipment" };
            List<int> deads = new List<int>() { 1, 2, 0, 1, 0, 1, 0, 1, 0, 0 };
            List<int> inj = new List<int>() { 4, 3, 2, 1, 4, 2, 3, 1, 2, 2 };
            if (!incidents)
            {
                for (int i = 0; i < addresses.Count; i++)
                {
                   
                    var team = firemanTeamRepository.GetByName(teamnames[i % 6]);
                    FireIncident incident = new FireIncident()
                    {
                        Address = addresses[i],
                        Status = IncidentStatus.Done,
                        Date = DateTime.Now.AddDays(-(i * 200)),
                        Reason = reasons[i],
                        Injured = inj[i],
                        Dead =deads[i],
                        FiremanTeam = team
                    };
                    fireIncidentRepository.Save(incident);
                }               
            }
        }
    }
}

