using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private CandidateElectionRepository _candidateElectionRepository;
        private UserService _userService;
        private IMapper _mapper;
        [TempData]
        public string Message { get; set; }

        public ElectionsController(CitizenRepository citizenRepository,
            CandidateRepository candidatesRepository,
            ElectionRepository electionRepository,
            CandidateElectionRepository candidateElectionRepository,
            UserService userService, IMapper mapper)
        {
            _citizenRepository = citizenRepository;
            _candidateRepository = candidatesRepository;
            _electionRepository = electionRepository;
            _candidateElectionRepository = candidateElectionRepository;
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
            var viewModel = _electionRepository.GetAll()
                 .Select(x => new ElectionViewModel()
                   {
                       Name = x.Name,
                       Description = x.Description,
                       Start = x.Start,
                       End = x.End,
                       Id = x.Id
                   }).ToList();

            return View(viewModel);
        }
        
        [HttpGet]
        [Authorize]
        [Route("Elections/Details/{electionId}")]

        public  IActionResult Details(long electionId)
        {
            var candidates = _candidateElectionRepository.GetCandidatesByElectionId(electionId);
            var election   = _electionRepository.Get(electionId);

            CandidateElectionViewModel viewModel = new CandidateElectionViewModel()
            {
                Election = election,
                Candidates = candidates
            };
            
            return View(viewModel);
        }
        
  
        
        [HttpPost]
      [Route("Elections/Register/{id}")]
        public IActionResult Register([FromRoute] long id, CandidateViewModel newCandidate)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var candidate = _mapper.Map<Candidate>(newCandidate);
         
            var election = _electionRepository.Get(id);
            
            var isRegistered =  _candidateElectionRepository.Add(candidate, election);
            if (isRegistered == true)
            {
                Message = $" {candidate.Name}, Вы уже зарегистрированы в выборах № {id}. Проверьте свое имя в списке кандидатов";
            }
            else
            {
                Message = $"{candidate.Name}, Спасибо за регистрацию!";
            }
            
            return RedirectToAction("Details", "Elections",  new { id = id });
        }

        
        public JsonResult DeleteElections(long id)
        {
            var elections =  _electionRepository.Get(id);
            if (elections == null)
            {
                return  Json(false);
            }

            _electionRepository.Remove(elections);

            return Json(true);
        }
        [HttpPost("Elections/DeleteCandidate/{candidateId}/{electionId}")]
         public JsonResult DeleteCandidate(long candidateId, long electionId)
        {

            var result =  _candidateElectionRepository.RemoveCandidateOfElection(candidateId, electionId );

            return Json(result);
        }


        // [HttpPost]
        public IActionResult Edit(string name)
        {
            var candidate = _candidateRepository.GetByName(name);

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ElectionViewModel model)
        {

            var election = _mapper.Map<Election>(model);
            _electionRepository.Save(election);

            return RedirectToAction("Index", "Elections");
        }

        public JsonResult Vote(string name)
        {

            var citizen = _userService.GetUser();

            Ballot ballot = new Ballot
            {
              //  CitizenId = citizen.Id,
            };

            /*_votingRepository.Save(ballot);*/

            return Json(true);
        }
    }
}
