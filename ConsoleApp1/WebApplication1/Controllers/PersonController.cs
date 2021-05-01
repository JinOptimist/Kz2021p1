using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private IStudentRepository StudentRepository { get; set; }
        private IPupilRepository PupilRepository { get; set; }
        private IMapper Mapper { get; set; }

        public PersonController(IStudentRepository studentRepository, IPupilRepository pupilRepository, IMapper mapper)
        {
            _studentPresentation = studentPresentation;
            _pupilPresentation = pupilPresentation;
            _studentRepository = studentRepository;
            _pupilRepository = pupilRepository;
            _webHostEnvironment = webHostEnvironment;
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
                string message = $"Grant of student {student.Surname} {student.Name} {student.Patronymic}  was canceled ";
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

        [HttpGet]
        public IActionResult AddNewStudent()
        {
            var allFaculties = _studentRepository.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);

            ViewBag.Universities = new SelectList(_studentPresentation.GetListOfUniversityNames());
            return View();
        }

        [HttpGet]
        public IActionResult EditStudentData(long IDStudent)
        {
            var student = _studentPresentation.GetStudentById(IDStudent);

            var allFaculties = _studentRepository.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);

            ViewBag.Universities = new SelectList(_studentPresentation.GetListOfUniversityNames());

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrEditStudent(StudentViewModel studentViewModel)
        {
            var university = _studentPresentation.GetUniversityByUniversityName(studentViewModel.University.Name);
            studentViewModel.UniversityId = university.Id;
            studentViewModel.University = null;

            if (studentViewModel.AvatarFile != null)
            {
                var fileExtention = Path.GetExtension(studentViewModel.AvatarFile.FileName);
                var fileName = $"{studentViewModel.Id}{fileExtention}";
                var path = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "Image", "Avatars", fileName);
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await studentViewModel.AvatarFile.CopyToAsync(fileStream);
                }
                studentViewModel.Avatar = $"/Image/Avatars/{fileName}";
            }
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

        [HttpGet]
        public IActionResult AddNewPupil()
        {
            ViewBag.Schools = new SelectList(_pupilPresentation.GetListOfSchoolNames());
            return View();
        }

        [HttpGet]
        public IActionResult EditPupilData(long IDPupil)
        {
            var pupil = _pupilPresentation.GetPupilById(IDPupil);

            ViewBag.Schools = new SelectList(_pupilPresentation.GetListOfSchoolNames());
            return View(pupil);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrEditPupil(PupilViewModel pupilViewModel)
        {
            var school = _pupilPresentation.GetSchoolBySchoolName(pupilViewModel.School.Name);
            pupilViewModel.SchoolId = school.Id;
            pupilViewModel.School = null;

            if (pupilViewModel.AvatarFile != null)
            {
                var fileExtention = Path.GetExtension(pupilViewModel.AvatarFile.FileName);
                var fileName = $"{pupilViewModel.Id}{fileExtention}";
                var path = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "Image", "Avatars", fileName);
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await pupilViewModel.AvatarFile.CopyToAsync(fileStream);
                }
                pupilViewModel.Avatar = $"/Image/Avatars/{fileName}";
            }

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
            var universityIds = _studentPresentation.GetListOfUniversityIds();
            _pupilPresentation.GetPupilGrant(universityIds, minValueForGrant);

            return RedirectToAction("PupilList");
        }

        public IActionResult EndOfStudy()
        {
            _pupilPresentation.EndStudyYearForSchool();
            _studentPresentation.EndStudyYearForUniversity();
            return View();
        }
    }
}
