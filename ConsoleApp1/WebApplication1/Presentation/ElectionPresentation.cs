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


        public bool RemoveElections(long id)
        {
            var elections = _electionRepository.Get(id);
            if (elections != null)
            {
                _electionRepository.Remove(elections);
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

        public void CreateVote(Citizen citizen, Election election, Candidate candidate)
        {
            var ballot = new Ballot
            {
                Citizen = citizen,
                Election = election,
                VoteTime = DateTime.Now,
                Candidate = candidate
            };

            _ballotRepository.Save(ballot);
        }
    }
}