using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private StudentPresentation _studentPresentation;
        private PupilPresentation _pupilPresentation;
        private StudentRepository _studentRepository;
        private PupilRepository _pupilRepository;
        public PersonController(StudentPresentation studentPresentation, PupilPresentation pupilPresentation, StudentRepository studentRepository, PupilRepository pupilRepository)
        {
            _studentPresentation = studentPresentation;
            _pupilPresentation = pupilPresentation;
            _studentRepository = studentRepository;
            _pupilRepository = pupilRepository;
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

        public IActionResult StudentFullInfo(long studentId)
        {
            var studentViewModel = _studentPresentation.GetStudentById(studentId);
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

            return View("StudentListAndSearch");
        }

        [HttpGet]
        public IActionResult StudentGrantIndividual(long studentId)
        {

            var student = _studentPresentation.GetStudentById(studentId);
            if (student.OnGrant == true)
            {
                _studentPresentation.GetStudentGrantIndividual(student.Id, false);
                //ViewBag.Message = $"Grant of student {student.Surname} {student.Name} {student.Patronymic}  was canceled ";
                string message= $"Grant of student {student.Surname} {student.Name} {student.Patronymic}  was canceled ";
                return Json(message);
            }
            else
            {
                _studentPresentation.GetStudentGrantIndividual(student.Id, true);
                //ViewBag.Message = $"Grant was issued to student {student.Surname} {student.Name} {student.Patronymic}";
                string message = $"Grant was issued to student {student.Surname} {student.Name} {student.Patronymic}";
                return Json(message);
            }

            //return View("StudentListAndSearch");
        }

        public IActionResult AddNewStudent()
        {

            var allFaculties = _studentRepository.GetAllFaculties();
            var test = new SelectList(allFaculties);
            ViewBag.Faculties = test;
            return View();
        }

        public IActionResult EditStudentData(long IDStudent)
        {
            var student = _studentPresentation.GetStudentById(IDStudent);
            return View(student);
        }

        [HttpPost]
        public IActionResult AddNewOrEditStudent(StudentViewModel studentViewModel)
        {
            _studentPresentation.GetAddNewOrEditStudent(studentViewModel);
            return RedirectToAction("StudentList");
        }

        public JsonResult RemoveStudent(string iin)
        {
            Thread.Sleep(2000);

            // var student = _studentPresentation.GetStudentFullInfo(iin);
            var student = _studentRepository.GetStudentByIIN(iin);
            if (student == null)
            {
                return Json(false);
            }

            // _studentPresentation.RemoveStudent(student);
            _studentRepository.Remove(student);

            return Json(true);
        }

        public IActionResult PupilList(int page = 1)
        {
            var pupilViewModels = _pupilPresentation.GetPupilList(page);
            return View("PupilListAndSearch", pupilViewModels);
        }

        [HttpGet]
        public IActionResult PupilListAndSearch(string searchBy, string searchPupil, int page = 1)
        {
            ViewData["GetStudentDetails"] = searchPupil;

            var pupilViewModels = _pupilPresentation.GetPupilListAndSearch(searchBy, searchPupil, page);
            return View(pupilViewModels);
        }

        public IActionResult PupilFullInfo(long pupilId)
        {
            var pupilViewModel = _pupilPresentation.GetPupilById(pupilId);
            return View(pupilViewModel);
        }

        public IActionResult AddNewPupil()
        {
            return View();
        }

        public IActionResult EditPupilData(long IDPupil)
        {
            var pupil = _pupilPresentation.GetPupilById(IDPupil);
            return View(pupil);
        }

        [HttpPost]
        public IActionResult AddNewOrEditPupil(PupilViewModel pupilViewModel)
        {
            _pupilPresentation.GetAddNewOrEditPupil(pupilViewModel);
            return RedirectToAction("PupilList");
        }

        public JsonResult RemovePupil(string iin)
        {
            Thread.Sleep(2000);

            //var pupil = _pupilPresentation.GetPupilFullInfo(iin);
            var pupil = _pupilRepository.GetPupilByIIN(iin);
            if (pupil == null)
            {
                return Json(false);
            }

            //_pupilPresentation.RemovePupil(pupil);
            _pupilRepository.Remove(pupil);

            return Json(true);
        }


        [HttpPost]
        public IActionResult PupilGrant(int minValueForGrant)
        {
            ViewData["PostMinValueForGrant"] = minValueForGrant;      

            _pupilPresentation.GetPupilGrant(minValueForGrant);

            return RedirectToAction("PupilList");
        }

        public IActionResult EndOfStudy()
        {
           // _pupilPresentation.EndStudyYearForSchool();
           // _studentPresentation.EndStudyYearForUniversity();
            return View();
        }
    }
}
