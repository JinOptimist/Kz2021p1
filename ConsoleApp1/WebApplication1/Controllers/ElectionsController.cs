using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class ElectionsController : Controller
    {
        private readonly ElectionPresentation _electionPresentation;


        public ElectionsController(ElectionPresentation electionPresentation)
        {
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
            var viewModel = _electionPresentation.Details(id);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegisterCandidate()
        {
            var viewModel = _electionPresentation.RegisterCandidate();

            return View(viewModel);
        }

        [HttpPost]
        [Route("Elections/RegisterCandidate/{id}")]
        public IActionResult RegisterCandidate([FromRoute] long id, CandidateViewModel newCandidate)
        {
            if (!ModelState.IsValid) return View(newCandidate);

            var isRegistered = _electionPresentation.GetRegisteredCandidate(id, newCandidate);

            if (isRegistered)
            {
                ModelState.AddModelError(nameof(CandidateViewModel.Name),
                    "Такой кандидат уже зарегистрирован на эти выборы");
                return View(newCandidate);
            }
            
            _electionPresentation.RegisterCandidate(id, newCandidate);
            
            return RedirectToAction("Details", "Elections", new {id});
        }


        public JsonResult DeleteElection(long id)
        {
            return Json(_electionPresentation.DeleteElection(id));
        }

        public JsonResult DeleteCandidate(long id)
        {
            return Json(_electionPresentation.DeleteCandidate(id));
        }

        [HttpGet]
        public IActionResult EditCandidate(long id)
        {
            var viewModel = _electionPresentation.EditCandidate(id);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCandidate(CandidateViewModel viewModel)
        {
            _electionPresentation.EditCandidate(viewModel);
            
            return RedirectToAction("Index", "Elections");
        }

        [HttpGet]
        public IActionResult EditElection(long id)
        {
            var viewModel = _electionPresentation.EditElection(id);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditElection(ElectionViewModel viewModel)
        {
            _electionPresentation.EditElection(viewModel);
            
            return RedirectToAction("Details", "Elections");
        }

        [HttpGet]
        public IActionResult CreateElection()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateElection(ElectionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            _electionPresentation.CreateElection(model);
            
            return RedirectToAction("Index", "Elections");
        }


        [Route("Elections/Vote/{electionId}/{candidateId}")]
        public JsonResult Vote([FromRoute] long electionId, long candidateId)
        {
            var usedBallot = _electionPresentation.GetUsedBallots(electionId);

            if (usedBallot != null) return Json(false);
            
            return Json(_electionPresentation.CreateVote(electionId, candidateId));
        }
    }
}