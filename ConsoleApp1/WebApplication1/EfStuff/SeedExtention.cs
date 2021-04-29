using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff
{
    public static class SeedExtention
    {
        public const string AdminName = "admin";

        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultElectionStuff(scope.ServiceProvider);
                CreateDefaultCitizen(scope.ServiceProvider);
                CreateDefaultAdress(scope.ServiceProvider);
            }
            
            return host;
        }

        private static void CreateDefaultAdress(IServiceProvider serviceProvider)
        {
        }

        private static void CreateDefaultCitizen(IServiceProvider serviceProvider)
        {
            var citizenRepository = serviceProvider.GetService<CitizenRepository>();
           
          

            var admin = citizenRepository.GetByName(AdminName);
            if (admin == null)
            {
                admin = new Citizen()
                {
                    Name = AdminName,
                    Password = "admin",
                    Age = 30,
                    Local = Local.Eng
                };
                citizenRepository.Save(admin);
            }
        }

        private static void CreateDefaultElectionStuff(IServiceProvider serviceProvider)
        {
            var candidateRepository = serviceProvider.GetService<CandidateRepository>();
            var citizenRepository = serviceProvider.GetService<CitizenRepository>();
            var electionRepository = serviceProvider.GetService<ElectionRepository>();
            var ballotRepository = serviceProvider.GetService<BallotRepository>();
            
            ballotRepository.RemoveAll();
            candidateRepository.RemoveAll();    
            citizenRepository.RemoveAll();
            electionRepository.RemoveAll();
            
            var  john = new Citizen
            {
                Name = "John Doe" ,
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
            
            var  johnCandidate = new Candidate
            {
                Name = john.Name,
                Age = john.Age,
                CitizenId = john.Id,
                Idea = Idea.Anarchist,
                City = City.Almaty,
                Slogan = "Lorem!",
                Job = "Учитель"
            };
            
           presidentElection.Candidates.Add(johnCandidate);
           candidateRepository.Save(johnCandidate);
           electionRepository.Save(presidentElection);

            var arthur = new Citizen
            {
                Name = "Arthur Poe" ,
                Password = "123!",
                Age = 30,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(arthur);
            
            var arthurCandidate = new Candidate
            {
                CitizenId = arthur.Id,
                Name = arthur.Name,
                Age = arthur.Age,
                Idea = Idea.Anarchist,
                City = City.Almaty,
                Slogan = "Lorem ipsum dolor sit",
                Job = "Предприниматель"
            };
            
            presidentElection.Candidates.Add(arthurCandidate);
            candidateRepository.Save(arthurCandidate);
            electionRepository.Save(presidentElection);
            
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
                CitizenId = wendy.Id,
                Name = wendy.Name,
                Age = wendy.Age,
                Idea = Idea.Libertarian,
                City = City.Nursultan,
                Slogan = "Lorem ipsum dolor sit",
                Job = "Таксист"
            };
            presidentElection.Candidates.Add(wendyCandidate);
            candidateRepository.Save(wendyCandidate);
            electionRepository.Save(presidentElection);
            
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
                CitizenId = julie.Id,
                Name = julie.Name,
                Age = julie.Age,
                Idea = Idea.Liberal,
                City = City.Karaganda,
                Slogan = "Lorem ipsum dolor sit am",
                Job = "Архитектор"
            };
            presidentElection.Candidates.Add(julieCandidate);
            candidateRepository.Save(julieCandidate);
            electionRepository.Save(presidentElection);
            
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
                CitizenId = erik.Id,
                Name = erik.Name,
                Age = erik.Age,
                Idea = Idea.Conservative,
                City = City.Almaty,
                Slogan = "Lorem ips",
                Job = "Программист"
            };
            presidentElection.Candidates.Add(erikCandidate);
            candidateRepository.Save(erikCandidate);
            electionRepository.Save(presidentElection);
            
            
            
            var sheriffElection = new Election
            {
                Name = "Sheriff Election",
                Start = DateTime.Now,
                Description = "Lorem dolor"
            };
            
            sheriffElection.End = presidentElection.Start.AddHours(8);
            electionRepository.Save(sheriffElection);
            
            var   jeremy = new Citizen
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
                CitizenId = jeremy.Id,
                Name = jeremy.Name,
                Age = jeremy.Age,
                Idea = Idea.Anarchist,
                City = City.Nursultan,
                Slogan = "Lorem ips",
                Job = "Строитель"
            };
            
            sheriffElection.Candidates.Add(jeremyCandidate);
            candidateRepository.Save(jeremyCandidate);
            electionRepository.Save(sheriffElection);

            sheriffElection.Candidates.Add(julieCandidate);
            candidateRepository.Save(julieCandidate);
            electionRepository.Save(sheriffElection);
            
            var ballot1 = new Ballot
            {
                CitizenId = john.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot1);
            
            
            var ballot2 = new Ballot
            {
                CitizenId = arthur.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot2);
            
            var ballot3 = new Ballot
            {
                CitizenId = wendy.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot3);
            
            var ballot4 = new Ballot
            {
                CitizenId = julie.Id,
                ElectionId = sheriffElection.Id,
                VoteTime = DateTime.Now
            };
            
            ballotRepository.Save(ballot4);
            
            
            var ballot5 = new Ballot
            {
                CitizenId = erik.Id,
                ElectionId = sheriffElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot5);
            
            
            johnCandidate.Ballots.Add(ballot1); 
            candidateRepository.Save(johnCandidate);
            
            arthurCandidate.Ballots.Add(ballot2);
            candidateRepository.Save(johnCandidate);
            
            wendyCandidate.Ballots.Add(ballot3);
            candidateRepository.Save(johnCandidate);
            
            julieCandidate.Ballots.Add(ballot4);
            candidateRepository.Save(johnCandidate);
            
            jeremyCandidate.Ballots.Add(ballot5);
            candidateRepository.Save(jeremyCandidate);
        }
    }
}