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

            var arthur = new Citizen
            {
                Name = "Arthur Poe" ,
                Password = "123!",
                Age = 30,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(arthur);

            var wendy = new Citizen
            {
                Name = "Wendy Toe",
                Password = "123!",
                Age = 21,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };
            citizenRepository.Save(wendy);

            var julie = new Citizen
            {
                Name = "Julie Doe",
                Password = "123!",
                Age = 34,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };
            citizenRepository.Save(julie);

            var erik = new Citizen
            {
                Name = "Erik Roe",
                Password = "123!",
                Age = 24,
                CreatingDate = DateTime.Now,
                Local = Local.Eng
            };
            citizenRepository.Save(erik);

            var   jeremy = new Citizen
            {
                Name = "Jeremy Moe",
                Password = "123!",
                Age = 44,
                CreatingDate = DateTime.Now,
                Local = Local.Rus
            };
            citizenRepository.Save(jeremy);
            
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
            candidateRepository.Save(johnCandidate);
            
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
            
            candidateRepository.Save(arthurCandidate);
            
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
            candidateRepository.Save(wendyCandidate);
            
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
            candidateRepository.Save(julieCandidate);
            
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
            candidateRepository.Save(erikCandidate);
            
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
            candidateRepository.Save(jeremyCandidate);
            
            var congressmanElection = new Election
            {
                Name = "Congressman Election",
                Start = DateTime.Now,
                Description = "Lorem dolor just to test archiving it will be empty elections"
            };
            congressmanElection.End = congressmanElection.Start.AddMilliseconds(100);
            electionRepository.Save(congressmanElection);
            
            var missUniverseElection = new Election
            {
                Name = "Miss Universe Election",
                Start = DateTime.Now,
                Description = "Dummy election dolor just to test archiving it will be empty elections"
            };
            missUniverseElection.End = missUniverseElection.Start.AddMilliseconds(20);
            electionRepository.Save(missUniverseElection);
            
            var presidentElection = new Election
            {
                Name = "President Election",
                Start = DateTime.Now,
                Description = "Lorem ipsum d"
                
            };
            presidentElection.End = presidentElection.Start.AddHours(24);
            electionRepository.Save(presidentElection);
            
            var sheriffElection = new Election
            {
                Name = "Sheriff Election",
                Start = DateTime.Now,
                Description = "Lorem dolor"
            };
            sheriffElection.End = presidentElection.Start.AddHours(8);
            electionRepository.Save(sheriffElection);
            
            var mayorElection = new Election
            {
                Name = "Mayor Election",
                Start = DateTime.Now,
                Description = "Lorem ipsum"
            };
            mayorElection.End = mayorElection.Start.AddDays(1);
            electionRepository.Save(mayorElection);
            
            //Выборы президента
            presidentElection.Candidates.Add(johnCandidate);
            electionRepository.Save(presidentElection);
            
            presidentElection.Candidates.Add(arthurCandidate);
            electionRepository.Save(presidentElection);
            
            presidentElection.Candidates.Add(wendyCandidate);
            electionRepository.Save(presidentElection);
            
            presidentElection.Candidates.Add(julieCandidate);
            electionRepository.Save(presidentElection);
            
            presidentElection.Candidates.Add(erikCandidate);
            electionRepository.Save(presidentElection);
            
            presidentElection.Candidates.Add(jeremyCandidate);
            electionRepository.Save(presidentElection);
            
            //Выборы шерифа
            sheriffElection.Candidates.Add(jeremyCandidate);
            electionRepository.Save(sheriffElection);
            
            sheriffElection.Candidates.Add(erikCandidate);
            electionRepository.Save(sheriffElection);
            
            sheriffElection.Candidates.Add(julieCandidate);
            electionRepository.Save(sheriffElection);
            
            //Выборы мэра
            mayorElection.Candidates.Add(wendyCandidate);
            electionRepository.Save(mayorElection);
            
            mayorElection.Candidates.Add(julieCandidate);
            electionRepository.Save(mayorElection);
            
            mayorElection.Candidates.Add(erikCandidate);
            electionRepository.Save(mayorElection);
            
            mayorElection.Candidates.Add(jeremyCandidate);
            electionRepository.Save(mayorElection);

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
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };
            
            ballotRepository.Save(ballot4);
            
            var ballot5 = new Ballot
            {
                CitizenId = erik.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot5);
            
            var ballot6 = new Ballot
            {
                CitizenId = jeremy.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot6);
            
            var ballot7 = new Ballot
            {
                CitizenId = john.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot7);
            
            var ballot8 = new Ballot
            {
                CitizenId = arthur.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot8);
            
            var ballot9 = new Ballot
            {
                CitizenId = wendy.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot9);
            
            var ballot10 = new Ballot
            {
                CitizenId = julie.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };
            
            ballotRepository.Save(ballot10);
            
            var ballot11 = new Ballot
            {
                CitizenId = erik.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot11);
            
            var ballot12 = new Ballot
            {
                CitizenId = jeremy.Id,
                ElectionId = presidentElection.Id,
                VoteTime = DateTime.Now
            };

            ballotRepository.Save(ballot12);
            
            johnCandidate.Ballots.Add(ballot1);
            candidateRepository.Save(johnCandidate);
            
            johnCandidate.Ballots.Add(ballot2);
            candidateRepository.Save(johnCandidate);
            
            johnCandidate.Ballots.Add(ballot3);
            candidateRepository.Save(johnCandidate);
            
            johnCandidate.Ballots.Add(ballot4);
            candidateRepository.Save(johnCandidate);
            
            arthurCandidate.Ballots.Add(ballot5);
            candidateRepository.Save(arthurCandidate);
            
            arthurCandidate.Ballots.Add(ballot6);
            candidateRepository.Save(arthurCandidate);
            
            julieCandidate.Ballots.Add(ballot7);
            candidateRepository.Save(julieCandidate);
            
            erikCandidate.Ballots.Add(ballot8);
            candidateRepository.Save(erikCandidate);
            
            jeremyCandidate.Ballots.Add(ballot9);
            candidateRepository.Save(jeremyCandidate);
            
            jeremyCandidate.Ballots.Add(ballot10);
            candidateRepository.Save(jeremyCandidate);
            
            wendyCandidate.Ballots.Add(ballot11);
            candidateRepository.Save(wendyCandidate);

        }
    }
}