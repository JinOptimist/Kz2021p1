using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Presentation
{
    public class ElectionPresentation
    {
        private readonly IBallotRepository _ballotRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IElectionRepository _electionRepository;
        private readonly IMapper _mapper;
        private IUserService _userService;

        public ElectionPresentation(
            ICandidateRepository candidatesRepository,
            IElectionRepository electionRepository,
            IBallotRepository ballotRepository,
            IUserService userService,
            IMapper mapper)
        {
            _candidateRepository = candidatesRepository;
            _electionRepository = electionRepository;
            _ballotRepository = ballotRepository;
            _userService = userService;
            _mapper = mapper;
        }


        public List<ElectionViewModel> GetActiveElections()
        {
            return _electionRepository.GetAll()
                .Where(x => x.End > DateTime.Now)
                .Select(x => _mapper.Map<ElectionViewModel>(x))
                .ToList();
        }

        public List<ElectionViewModel> GetArchivedElections()
        {
            return _electionRepository.GetAll()
                .Where(x => x.End < DateTime.Now)
                .Select(x => _mapper.Map<ElectionViewModel>(x))
                .ToList();
        }

        public Ballot GetUsedBallots(long citizenId, long electionId)
        {
            return _ballotRepository.GetAll()
                .FirstOrDefault(
                    c => c.Citizen.Id == citizenId
                         && c.Election.Id == electionId);
        }


        public bool DeleteElection(long id)
        {
            var election = _electionRepository.Get(id);
            if (election != null)
            {
                _electionRepository.Remove(election);
                return true;
            }

            return false;
        }

        public bool DeleteCandidate(long id)
        {
            var candidate = _candidateRepository.Get(id);
            if (candidate != null)
            {
                _candidateRepository.Remove(candidate);
                return true;
            }

            return false;
        }

      

        public ElectionViewModel Details(long id)
        {
            var election = _electionRepository.Get(id);
            var viewModel = _mapper.Map<ElectionViewModel>(election);
            
            var citizen = _userService.GetUser();
            
            viewModel.IsVoted = GetUsedBallots(citizen.Id, election.Id) != null;

            return viewModel;
        }

        public CandidateViewModel RegisterCandidate()
        {
            var citizen = _userService.GetUser();

            return new CandidateViewModel
            {
                Name = citizen.Name,
                Age = citizen.Age
            };

        }
        
        public bool RegisterCandidate(long id, CandidateViewModel newCandidate)
        {
            var election = _electionRepository.Get(id);
            var citizen = _userService.GetUser();
           
            var candidate = _mapper.Map<Candidate>(newCandidate);
            candidate.Election = election;
            candidate.Citizen = citizen;
           
            if (election.Candidates.Any(x => x.Citizen.Id == candidate.Citizen.Id))
            {
                return false;
            }
           
            _candidateRepository.Save(candidate);
            return true;
        }

        public CandidateViewModel EditCandidate(long id)
        {
            var candidate = _candidateRepository.Get(id);
            return _mapper.Map<CandidateViewModel>(candidate);
        }
        
        public void EditCandidate(CandidateViewModel viewModel)
        {
            var candidate = _mapper.Map<Candidate>(viewModel);
            _candidateRepository.Save(candidate);
        }

        public ElectionViewModel EditElection(long id)
        {
            var election = _electionRepository.Get(id);
            return _mapper.Map<ElectionViewModel>(election);
        }
        
        public void EditElection(ElectionViewModel viewModel)
        {
            var election = _mapper.Map<Election>(viewModel);
            _electionRepository.Save(election);
        }

        public void CreateElection(ElectionViewModel model)
        {
            var election = _mapper.Map<Election>(model);
            _electionRepository.Save(election);

        }
        
        public bool CreateVote(long electionId, long candidateId)
        {
            
            var citizen = _userService.GetUser();

            var election = _electionRepository.Get(electionId);

            var usedBallot = GetUsedBallots(citizen.Id, electionId);

            if (usedBallot != null) return false;

            var candidate = _candidateRepository.Get(candidateId);
            
            var ballot = new Ballot
            {
                Citizen = citizen,
                Election = election,
                VoteTime = DateTime.Now,
                Candidate = candidate
            };

            _ballotRepository.Save(ballot);
            return true;
        }
    }
}