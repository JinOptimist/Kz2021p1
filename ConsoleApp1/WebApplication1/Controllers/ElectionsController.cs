using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Presentation;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ElectionsController : Controller
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ElectionPresentation _electionPresentation;
        private readonly IElectionRepository _electionRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public ElectionsController(
            ElectionPresentation electionPresentation,
            ICandidateRepository candidatesRepository,
            IElectionRepository electionRepository,
            IUserService userService,
            IMapper mapper)
        {
            _candidateRepository = candidatesRepository;
            _electionRepository = electionRepository;
            _userService = userService;
            _mapper = mapper;
            _electionPresentation = electionPresentation;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var activeElections = _electionPresentation.GetActiveElections();

            return View(activeElections);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Archive()
        {
            var archivedElections = _electionPresentation.GetArchivedElections();

            return View(archivedElections);
        }

        [HttpGet]
        [Authorize]
        [Route("Elections/Details/{id}")]
        public IActionResult Details(long id)
        {
            var election = _electionRepository.Get(id);
            var viewModel = _mapper.Map<ElectionViewModel>(election);

            var citizen = _userService.GetUser();

            viewModel.IsVoted = _electionPresentation.GetUsedBallots(citizen.Id, election.Id) != null;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var user = _userService.GetUser();

            var viewModel = _mapper.Map<CandidateViewModel>(user);

            return View(viewModel);
        }

        [HttpPost]
        [Route("Elections/Register/{id}")]
        public IActionResult Register([FromRoute] long id, CandidateViewModel newCandidate)
        {
            if (!ModelState.IsValid) return View(newCandidate);

            var election = _electionRepository.Get(id);
            var citizen = _userService.GetUser();

            //   newCandidate.Election = election;
            //   newCandidate.Citizen = citizen; 
            //   var candidate = _mapper.Map<Candidate>(newCandidate);

            var candidate = new Candidate
            {
                Name = newCandidate.Name,
                Election = election,
                Citizen = citizen,
                Age = newCandidate.Age,
                Slogan = newCandidate.Slogan,
                Idea = newCandidate.Idea
            };


            if (election.Candidates.Any(x => x.Citizen.Id == candidate.Citizen.Id))
            {
                ModelState.AddModelError(nameof(CandidateViewModel.Name),
                    "Такой кандидат уже зарегистрирован на эти выборы");
                return View(newCandidate);
            }

            _candidateRepository.Save(candidate);

            return RedirectToAction("Details", "Elections", new {id});
        }


        public JsonResult DeleteElections(long id)
        {
            return Json(_electionPresentation.RemoveElections(id));
        }

        public JsonResult DeleteCandidate(long id)
        {
            return Json(_electionPresentation.DeleteCandidate(id));
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
            if (!ModelState.IsValid) return View(model);

            var election = _mapper.Map<Election>(model);
            _electionRepository.Save(election);

            return RedirectToAction("Index", "Elections");
        }


        [Route("Elections/Vote/{electionId}/{candidateId}")]
        public JsonResult Vote([FromRoute] long electionId, long candidateId)
        {
            var citizen = _userService.GetUser();

            var election = _electionRepository.Get(electionId);

            var usedBallot = _electionPresentation.GetUsedBallots(citizen.Id, electionId);

            var candidate = _candidateRepository.Get(candidateId);

            if (usedBallot != null) return Json(false);

            _electionPresentation.CreateVote(citizen, election, candidate);
            
            return Json(true);
        }
    }
}