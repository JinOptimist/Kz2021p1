using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Namotion.Reflection;
using Newtonsoft.Json;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Presentation;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ElectionsController : Controller
    {
        private CitizenRepository _citizenRepository;
        private CandidateRepository _candidateRepository;
        private ElectionRepository _electionRepository;
        private BallotRepository _ballotRepository;
        private UserService _userService;
        private IMapper _mapper;

        
        public ElectionsController(CitizenRepository citizenRepository,
            CandidateRepository candidatesRepository,
            ElectionRepository electionRepository,
            BallotRepository ballotRepository,
            UserService userService, IMapper mapper)
        {
            _citizenRepository = citizenRepository;
            _candidateRepository = candidatesRepository;
            _electionRepository = electionRepository;
            _ballotRepository = ballotRepository;
            _userService = userService;
            _mapper = mapper;
        }
       
        public IActionResult Register()
        {
            var user = _userService.GetUser();

            var viewModel = _mapper.Map<CandidateViewModel>(user);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var elections = _electionRepository.GetAll();
            var viewModel = _mapper.Map<List<ElectionViewModel>>(elections);

            List<ElectionViewModel> activeElections = new List<ElectionViewModel>();

            foreach (var election in viewModel)
            {
                if (election.End > DateTime.Now)
                {
                    activeElections.Add(election);
                }
            }
          
            return View(activeElections);
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Archive()
        {
            var elections = _electionRepository.GetAll();
            var viewModel = _mapper.Map<List<ElectionViewModel>>(elections);

             List<ElectionViewModel> archivedElections = new List<ElectionViewModel>();

            foreach (var election in viewModel)
            {
                if (election.End < DateTime.Now)
                {
                    archivedElections.Add(election);
                }
            }
          
            return View(archivedElections);
        }
        
        [HttpGet]
        [Authorize]
        [Route("Elections/Details/{id}")]

        public  IActionResult Details(long id)
        {
            var election = _electionRepository.Get(id);
            var viewModel = _mapper.Map<ElectionViewModel>(election);
            
            var citizen = _userService.GetUser();

            var ballots = _ballotRepository.GetAll();
                
            var usedBallot = ballots.FirstOrDefault(
                c => c.CitizenId == citizen.Id
                     && c.ElectionId == election.Id);
            if (usedBallot != null)
            {
                viewModel.IsVoted = true;
            }
            
            return View(viewModel);
        }
        
        
        [HttpPost]
        [Route("Elections/Register/{id}")]
        public IActionResult Register([FromRoute] long id, CandidateViewModel newCandidate)
        {
            if (!ModelState.IsValid)
            {
                return View(newCandidate);
            }
            var candidate = _mapper.Map<Candidate>(newCandidate);
         
            var election = _electionRepository.Get(id);

            if (election.Candidates.Any(x => x.CitizenId == candidate.CitizenId))
            {
                ModelState.AddModelError(nameof(CandidateViewModel.Name),
                    "Такой кандидат уже зарегистрирован на эти выборы");
                return View(newCandidate);
            }
            
            election.Candidates.Add(candidate);
            _candidateRepository.Save(candidate);
            
            return RedirectToAction("Details", "Elections",  new { id = id });
        }

        
        public JsonResult DeleteElections(long id)
        {
            try
            {
                var elections =  _electionRepository.Get(id);
                _electionRepository.Remove(elections);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex.Message); 
            }
        }
        
        public JsonResult DeleteCandidate(long id)
        {
            try
            {
                var candidate = _candidateRepository.Get(id);
                _candidateRepository.Remove(candidate);
                return Json(true); 
            }
            catch (Exception ex)
            {
                return Json(ex.Message); 
            }
        }

        [HttpGet]
        public IActionResult EditCandidate(long id)
        {
            var candidate = _candidateRepository.Get(id);
            var viewModel = _mapper.Map<CandidateViewModel>(candidate);
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditCandidate(CandidateViewModel viewModel)
        {
            var candidate = _mapper.Map<Candidate>(viewModel);
            _candidateRepository.Save(candidate);
            
            return RedirectToAction("Index", "Elections");
        }
        
        [HttpGet]
        public IActionResult EditElection(long id)
        {
            var election = _electionRepository.Get(id);
            var viewModel = _mapper.Map<ElectionViewModel>(election);
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult EditElection(ElectionViewModel viewModel)
        {
            var election = _mapper.Map<Election>(viewModel);
            
            _electionRepository.Save(election);
            
            return RedirectToAction("Details", "Elections");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ElectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var election = _mapper.Map<Election>(model);
            _electionRepository.Save(election);

            return RedirectToAction("Index", "Elections");
        }
        
        
        [Route("Elections/Vote/{electionId}/{candidateId}")]
        public JsonResult Vote([FromRoute] long electionId, long candidateId)
        {
            try
            {
                var citizen = _userService.GetUser();
                var candidate = _candidateRepository.Get(candidateId);
                var election = _electionRepository.Get(electionId);

                var ballots = _ballotRepository.GetAll();
                
                var usedBallot = ballots.FirstOrDefault(
                    c => c.CitizenId == citizen.Id
                         && c.ElectionId == electionId);

                if (usedBallot == null)
                {
                    var ballot = new Ballot
                    {
                        CitizenId = citizen.Id,
                        ElectionId = election.Id,
                        VoteTime = DateTime.Now
                    };
            
                    candidate.Ballots.Add(ballot);
                    _candidateRepository.Save(candidate);
                    _ballotRepository.Save(ballot);
                
                    return Json(new {message = "👍"}); 
                }

                return Json(new { message = "Вы уже проголосовали"});

            }
            catch (Exception ex)
            {
                return Json( ex.Message);
            }
        }
    }
}