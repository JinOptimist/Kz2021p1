using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;
using WebApplication1.Models.Education;
using WebApplication1.Presentation;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private IStudentPresentation _studentPresentation;
        private IPupilPresentation _pupilPresentation;
        private IWebHostEnvironment _webHostEnvironment;

        public PersonController(IStudentPresentation studentPresentation, IPupilPresentation pupilPresentation, IWebHostEnvironment webHostEnvironment)
        {
            _studentPresentation = studentPresentation;
            _pupilPresentation = pupilPresentation;
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
            return Json(_studentPresentation.GetStudentGrantIndividual(studentId));
        }

        [HttpGet]
        public IActionResult AddNewStudent()
        {
            var allFaculties = _studentPresentation.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);

            ViewBag.Universities = new SelectList(_studentPresentation.GetListOfUniversityNames());
            return View();
        }

        [HttpGet]
        public IActionResult EditStudentData(long studentId)
        {
            var student = _studentPresentation.GetStudentById(studentId);

            var allFaculties = _studentPresentation.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);

            ViewBag.Universities = new SelectList(_studentPresentation.GetListOfUniversityNames());

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrEditStudentAsync(StudentViewModel studentViewModel)
        {
            if (studentViewModel.AvatarFile != null)
            {
                var fileExtention = Path.GetExtension(studentViewModel.AvatarFile.FileName);
                var fileName = $"{studentViewModel.Iin.Substring(6)}{fileExtention}";
                var path = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    "Image", "Avatars", fileName);
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await studentViewModel.AvatarFile.CopyToAsync(fileStream);
                }
                studentViewModel.AvatarUrl = $"/Image/Avatars/{fileName}";
            }
            _studentPresentation.GetAddNewOrEditStudentAsync(studentViewModel);
            return RedirectToAction("StudentList");
        }
        public JsonResult RemoveStudent(string iin)
        {
            return Json(_studentPresentation.Remove(iin));
        }

        [HttpGet]
        public IActionResult Certificate(int page = 1)
        {
            var viewModels = _studentPresentation.GetStudentList(page);
            viewModels.Action = "Certificate";

            var allFaculties = _studentPresentation.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);

            var certificateTypes = new List<string> { "Police", "Medicine" };
            ViewBag.CertificateTypes = certificateTypes;

            return View(viewModels);
        }
        [HttpGet]
        public IActionResult SearchStudentByFacultyAndCourseYear(string faculty, int courseYear, int page = 1)
        {
            var studentViewModels = _studentPresentation.GetStudentByFacultyAndCourseYear(faculty, courseYear, page);
            var allFaculties = _studentPresentation.GetAllFaculties();
            ViewBag.Faculties = new SelectList(allFaculties);
            return View("Certificate", studentViewModels);
        }

        public JsonResult AddNewCertificate(string iin, string selectedCertificateType)
        {
            return Json(_studentPresentation.AddNewCertificate(iin, selectedCertificateType));
        }

        public JsonResult CancelCertificate(string iin, string certificateType)
        {
            return Json(_studentPresentation.CancelCertificate(iin, certificateType));
        }

        public IActionResult PupilList(int page = 1)
        {
            var pupilViewModels = _pupilPresentation.GetPupilList(page);
            return View("PupilListAndSearch", pupilViewModels);
        }

        [HttpGet]
        public IActionResult PupilListAndSearch(string searchBy, string searchPupil, int page = 1)
        {
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
        public IActionResult EditPupilData(long pupilId)
        {
            var pupil = _pupilPresentation.GetPupilById(pupilId);

            ViewBag.Schools = new SelectList(_pupilPresentation.GetListOfSchoolNames());
            return View(pupil);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewOrEditPupil(PupilViewModel pupilViewModel)
        {
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
                pupilViewModel.AvatarUrl = $"/Image/Avatars/{fileName}";
            }

            _pupilPresentation.GetAddNewOrEditPupil(pupilViewModel);
            return RedirectToAction("PupilList");
        }

        public JsonResult RemovePupil(string iin)
        {
            return Json(_pupilPresentation.Remove(iin));
        }


        [HttpPost]
        public IActionResult PupilGrant(int minValueForGrant)
        {
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
