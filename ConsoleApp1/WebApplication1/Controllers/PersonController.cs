using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonController : Controller
    {
        private StudentRepository StudentRepository { get; set; }
        private PupilRepository PupilRepository { get; set; }
        private IMapper Mapper { get; set; }

        public PersonController(StudentRepository studentRepository, PupilRepository pupilRepository, IMapper mapper)
        {
            StudentRepository = studentRepository;
            PupilRepository = pupilRepository;
            Mapper = mapper;
        }

        public IActionResult StudentListAndSearch(int page = 1)
        {
            var students = StudentRepository
                .GetAll()
                .Select(x => Mapper.Map<StudentViewModel>(x))
                .ToList();
            var model = PagingList.Create(students, 3, page);

            model.Action = "StudentListAndSearch";
            return View(model);
        }

        [HttpGet]
        public IActionResult StudentListAndSearch(string searchBy, string searchStudent, int page = 1)
        {
            ViewData["GetStudentDetails"] = searchStudent;

            var query = from x in StudentRepository.GetAll() select x;
            if (!String.IsNullOrEmpty(searchStudent))
            {
                if (searchBy == "iin")
                {
                    query = query.Where(x => x.IIN.Equals(searchStudent));
                }
                if (searchBy == "name")
                {
                    query = query.Where(x => x.Name.Contains(searchStudent));
                }
                if (searchBy == "courseYear")
                {
                    query = query.Where(x => x.CourseYear == int.Parse(searchStudent)).OrderBy(s => s.UniversityId);
                }
                if (searchBy == "universityID")
                {
                    query = query.Where(x => x.UniversityId == int.Parse(searchStudent)).OrderBy(s => s.CourseYear);
                }
            }
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
            foreach (var item in query)
            {
                studentViewModels.Add(Mapper.Map<StudentViewModel>(item));
            }
            var model = PagingList.Create(studentViewModels, 3, page);
            model.Action = "StudentListAndSearch";

            return View(model);
        }

        public IActionResult StudentFullInfo(string IINStudent)
        {
            var student = StudentRepository.GetStudentByIIN(IINStudent);
            var studentViewModel = Mapper.Map<StudentViewModel>(student);
            return View(studentViewModel);
        }


        [HttpPost]
        public IActionResult StudentListAndSearch(string select, double minGpaValue)
        {
            ViewData["minGpaValue"] = minGpaValue;

            if (minGpaValue != null)
            {
                if (select == "issueGrant")
                {
                    var studentsNoGrant = StudentRepository.GetAll()
                         .Where(x => x.OnGrant == false)
                         .Where(c => c.CourseYear > 1)
                         .Where(student => student.Gpa >= minGpaValue)
                         .ToList();
                    foreach (var student in studentsNoGrant)
                    {
                        StudentRepository.UpdateStudentGrantData(student.Id, true);
                    }
                }
                else
                {
                    var studentsYesGrant = StudentRepository.GetAll().Where(x => x.OnGrant == true).Where(student => student.Gpa <= minGpaValue).ToList();
                    foreach (var student in studentsYesGrant)
                    {
                        StudentRepository.UpdateStudentGrantData(student.Id, false);
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult StudentGrant(long IDStudent)
        {

            var student = StudentRepository.Get(IDStudent);
            if (student.OnGrant == true)
            {
                StudentRepository.UpdateStudentGrantData(student.Id, false);
            }
            else
            {
                StudentRepository.UpdateStudentGrantData(student.Id, true);
            }

            return View("StudentListAndSearch");
        }


        public IActionResult PupilListAndSearch()
        {
            var pupils = PupilRepository
                 .GetAll()
                 .Select(x => Mapper.Map<StudentViewModel>(x))
                 .ToList();
            return View(pupils);
        }


        [HttpGet]
        public async Task<IActionResult> PupilListAndSearch(string searchBy, string searchPupil)
        {
            ViewData["GetStudentDetails"] = searchPupil;

            var query = from x in PupilRepository.GetAll() select x;
            if (!String.IsNullOrEmpty(searchPupil))
            {
                if (searchBy == "iin")
                {
                    query = query.Where(x => x.IIN.Equals(searchPupil));
                }
                else if (searchBy == "name")
                {
                    query = query.Where(x => x.Name.Contains(searchPupil));
                }
                else if (searchBy == "classYear")
                {
                    query = query.Where(x => x.ClassYear == int.Parse(searchPupil)).OrderBy(s => s.SchoolId);
                }
                else if (searchBy == "schoolID")
                {
                    query = query.Where(x => x.SchoolId == int.Parse(searchPupil)).OrderBy(s => s.ClassYear);
                }
            }
            List<PupilViewModel> pupilViewModel = new List<PupilViewModel>();
            foreach (var item in query)
            {
                pupilViewModel.Add(Mapper.Map<PupilViewModel>(item));
            }
            return View(pupilViewModel);
        }

        public IActionResult PupilFullInfo(string IINPupil)
        {
            var pupil = PupilRepository.GetPupilByIIN(IINPupil);
            var pupilViewModel = Mapper.Map<PupilViewModel>(pupil);
            return View(pupilViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> PupilListAndSearch(int minValueForGrant)
        {
            ViewData["PostMinValueForGrant"] = minValueForGrant;

            // проверка значения инпута (если пользователь ввел букву например 
            // или текст а не числовое значение)

            // добавить вытаскивание айди универа рандомно
            // enum faculty добавить и тоже рандомно присвоивать при выдаче гранта и 
            // регистрации ученика в качестве студента


            if (minValueForGrant != null)
            {
                var pupils = PupilRepository.GetAll();
                foreach (var pupil in pupils)
                {
                    if (pupil.ENT != null)
                    {
                        StudentViewModel studentVIewModel = new StudentViewModel();
                        studentVIewModel.IIN = pupil.IIN;
                        studentVIewModel.Name = pupil.Name;
                        studentVIewModel.Surname = pupil.Surname;
                        studentVIewModel.Patronymic = pupil.Patronymic;
                        studentVIewModel.Birthday = pupil.Birthday;
                        studentVIewModel.Email = pupil.Email;
                        studentVIewModel.Faculty = "Biology";
                        studentVIewModel.CourseYear = 1;
                        studentVIewModel.Gpa = 0.0;
                        studentVIewModel.EnteredYear = DateTime.Now;
                        studentVIewModel.GraduatedYear = null;
                        studentVIewModel.UniversityId = 100; // Random()

                        if (pupil.ENT >= minValueForGrant)
                        {
                            studentVIewModel.OnGrant = true;
                            var student = Mapper.Map<Student>(studentVIewModel);
                            StudentRepository.Save(student);

                            PupilRepository.Remove(pupil);
                        }
                        else
                        {
                            studentVIewModel.OnGrant = false;
                            var student = Mapper.Map<Student>(studentVIewModel);
                            StudentRepository.Save(student);

                            PupilRepository.Remove(pupil);
                        }
                    }
                }
            }

            return View();
        }
    }
}
