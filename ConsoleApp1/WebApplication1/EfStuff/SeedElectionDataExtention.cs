using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff
{
    public static class SeedElectionDataExtention
    {
        public static IHost SeedElectionData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultElectionStuff(scope.ServiceProvider);
            }

            return host;
        }

        private static void CreateDefaultElectionStuff(IServiceProvider serviceProvider)
        {
            var candidateRepository = serviceProvider.GetService<ICandidateRepository>();
            var citizenRepository = serviceProvider.GetService<ICitizenRepository>();
            var electionRepository = serviceProvider.GetService<IElectionRepository>();
            var ballotRepository = serviceProvider.GetService<IBallotRepository>();

            ballotRepository.RemoveAll();
            candidateRepository.RemoveAll();
            citizenRepository.RemoveAll();
            electionRepository.RemoveAll();

            var john = new Citizen
            {
                Name = "John Doe",
                Password = "123!",
                Age = 20,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(john);


            var presidentElection = new Election
            {
                Name = "President Election",
                Start = DateTime.Now,
                Description = "Lorem ipsum d"
            };
            presidentElection.End = presidentElection.Start.AddHours(24);

            electionRepository.Save(presidentElection);

            var johnCandidate = new Candidate
            {
                Name = john.Name,
                Age = john.Age,
                Citizen = john,
                Idea = Idea.Anarchist,
                City = City.Almaty,
                Slogan = "Lorem!",
                Job = "Учитель",
                Election = presidentElection
            };

            candidateRepository.Save(johnCandidate);


            var arthur = new Citizen
            {
                Name = "Arthur Poe",
                Password = "123!",
                Age = 30,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(arthur);

            var arthurCandidate = new Candidate
            {
                Citizen = arthur,
                Name = arthur.Name,
                Age = arthur.Age,
                Idea = Idea.Anarchist,
                City = City.Almaty,
                Slogan = "Lorem ipsum dolor sit",
                Job = "Предприниматель",
                Election = presidentElection
            };

            candidateRepository.Save(arthurCandidate);


            var wendy = new Citizen
            {
                Name = "Wendy Toe",
                Password = "123!",
                Age = 21,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };
            citizenRepository.Save(wendy);

            var wendyCandidate = new Candidate
            {
                Citizen = wendy,
                Name = wendy.Name,
                Age = wendy.Age,
                Idea = Idea.Libertarian,
                City = City.Nursultan,
                Slogan = "Lorem ipsum dolor sit",
                Job = "Таксист",
                Election = presidentElection
            };

            candidateRepository.Save(wendyCandidate);


            var julie = new Citizen
            {
                Name = "Julie Doe",
                Password = "123!",
                Age = 34,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };

            citizenRepository.Save(julie);

            var julieCandidate = new Candidate
            {
                Citizen = julie,
                Name = julie.Name,
                Age = julie.Age,
                Idea = Idea.Liberal,
                City = City.Karaganda,
                Slogan = "Lorem ipsum dolor sit am",
                Job = "Архитектор",
                Election = presidentElection
            };
            candidateRepository.Save(julieCandidate);


            var erik = new Citizen
            {
                Name = "Erik Roe",
                Password = "123!",
                Age = 24,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };
            citizenRepository.Save(erik);

            var erikCandidate = new Candidate
            {
                Citizen = erik,
                Name = erik.Name,
                Age = erik.Age,
                Idea = Idea.Conservative,
                City = City.Almaty,
                Slogan = "Lorem ips",
                Job = "Программист",
                Election = presidentElection
            };
            candidateRepository.Save(erikCandidate);


            var jeremy = new Citizen
            {
                Name = "Jeremy Moe",
                Password = "123!",
                Age = 44,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(jeremy);

            var jeremyCandidate = new Candidate
            {
                Citizen = jeremy,
                Name = jeremy.Name,
                Age = jeremy.Age,
                Idea = Idea.Anarchist,
                City = City.Nursultan,
                Slogan = "Lorem ips",
                Job = "Строитель",
                Election = presidentElection
            };


            candidateRepository.Save(jeremyCandidate);

            var sheriffElection = new Election
            {
                Name = "Sheriff Election",
                Start = DateTime.Now,
                Description = "Lorem"
            };
            sheriffElection.End = sheriffElection.Start.AddHours(24);

            electionRepository.Save(sheriffElection);


            var ballot1 = new Ballot
            {
                Citizen = john,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = johnCandidate
            };

            ballotRepository.Save(ballot1);


            var ballot2 = new Ballot
            {
                Citizen = arthur,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = arthurCandidate
            };

            ballotRepository.Save(ballot2);

            var ballot3 = new Ballot
            {
                Citizen = wendy,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = wendyCandidate
            };

            ballotRepository.Save(ballot3);

            var ballot4 = new Ballot
            {
                Citizen = julie,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = julieCandidate
            };

            ballotRepository.Save(ballot4);


            var ballot5 = new Ballot
            {
                Citizen = erik,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = erikCandidate
            };

            ballotRepository.Save(ballot5);

            var ballot6 = new Ballot
            {
                Citizen = jeremy,
                Election = presidentElection,
                VoteTime = DateTime.Now,
                Candidate = jeremyCandidate
            };

            ballotRepository.Save(ballot6);
        }
    }
}