using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private StudentRepository _studentRepository;
        private PupilRepository _pupilRepository;
        private StudentPresentation _studentPresentation;
        private PupilPresentation _pupilPresentation;
        private IMapper Mapper { get; set; }

        public PersonController(StudentRepository studentRepository, PupilRepository pupilRepository, StudentPresentation studentPresentation, IMapper mapper, PupilPresentation pupilPresentation)
        {
            _studentRepository = studentRepository;
            _pupilRepository = pupilRepository;
            _studentPresentation = studentPresentation;
            Mapper = mapper;
            _pupilPresentation = pupilPresentation;
        }

        public IActionResult StudentList(int page = 1)
        {
            var viewModels = _studentPresentation.GetStudentList(page);

            return View("StudentListAndSearch", viewModels);
        }

        [HttpGet]
        public IActionResult StudentListAndSearch(string searchBy, string searchStudent, int page = 1)
        {
            ViewData["GetStudentDetails"] = searchStudent;

            var viewModels = _studentPresentation.GetStudentListAndSearch(searchBy, searchStudent, page);
            return View(viewModels);
        }

        public IActionResult StudentFullInfo(string IINStudent)
        {
            var studentViewModel = _studentPresentation.GetStudentFullInfo(IINStudent);
            return View(studentViewModel);
        }


        [HttpPost]
        public IActionResult StudentGrantByGpa(string select, double minGpaValue)
        {
            ViewData["minGpaValue"] = minGpaValue;
            _studentPresentation.GetStudentGrantByGpa(select, minGpaValue);
            if (select == "issueGrant")
            {
                ViewBag.Message = $"Grant was issued to students with a Gpa greater than or equal to {minGpaValue}";
            }
            else
            {
                ViewBag.Message = $"Grant for students with a Gpa less than or equal to 3 has been canceled {minGpaValue}";
            }

            return RedirectToAction("StudentList");
        }

        [HttpGet]
        public IActionResult StudentGrantIndividual(long IDStudent)
        {

            var student = _studentRepository.Get(IDStudent);
            if (student.OnGrant == true)
            {
                _studentRepository.UpdateStudentGrantData(student.Id, false);
                ViewBag.Message = $"Grant of student {student.Surname} {student.Name} {student.Patronymic}  was canceled ";
            }
            else
            {
                _studentRepository.UpdateStudentGrantData(student.Id, true);
                ViewBag.Message = $"Grant was issued to student {student.Surname} {student.Name} {student.Patronymic}";
            }

            return RedirectToAction("StudentList");
        }

        public IActionResult AddNewStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewStudent(StudentViewModel studentViewModel)
        {
            _studentPresentation.GetAddNewStudent(studentViewModel);
            return View();
        }

        public JsonResult RemoveStudent(string iin)
        {
            Thread.Sleep(2000);

            var student = _studentRepository.GetStudentByIIN(iin);
            if (student == null)
            {
                return Json(false);
            }

            _studentRepository.Remove(student);

            return Json(true);
        }


        public IActionResult PupilList()
        {
            var pupilViewModels = _pupilPresentation.GetPupilList();
            return View("PupilListAndSearch", pupilViewModels);
        }

        [HttpGet]
        public IActionResult PupilListAndSearch(string searchBy, string searchPupil)
        {
            ViewData["GetStudentDetails"] = searchPupil;

            var pupilViewModels = _pupilPresentation.GetPupilListAndSearch(searchBy, searchPupil);
            return View(pupilViewModels);
        }

        public IActionResult PupilFullInfo(string IINPupil)
        {
            var pupilViewModel = _pupilPresentation.GetPupilFullInfo(IINPupil);
            return View(pupilViewModel);
        }

        public IActionResult AddNewPupil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewPupil(PupilViewModel pupilViewModel)
        {
            _pupilPresentation.GetAddNewPupil(pupilViewModel);
            return View();
        }

        public JsonResult RemovePupil(string iin)
        {
            Thread.Sleep(2000);

            var pupil = _pupilRepository.GetPupilByIIN(iin);
            if (pupil == null)
            {
                return Json(false);
            }

            _pupilRepository.Remove(pupil);

            return Json(true);
        }


        [HttpPost]
        public IActionResult PupilGrant(int minValueForGrant)
        {
            ViewData["PostMinValueForGrant"] = minValueForGrant;

            // проверка значения инпута (если пользователь ввел букву например 
            // или текст а не числовое значение)           

            _pupilPresentation.GetPupilGrant(minValueForGrant);

            return RedirectToAction("PupilList");
        }
    }
}
