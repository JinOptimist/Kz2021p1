using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class StudentPresentation : IStudentPresentation
    {
        private IStudentRepository _studentRepository;
        private IUniversityRepository _universityRepository;
        private ICertificateRepository _certificateRepository;
        private IMapper _mapper;

        public StudentPresentation(IStudentRepository studentRepository,
            IUniversityRepository universityRepository, IMapper mapper,
            ICertificateRepository certificateRepository)
        {
            _studentRepository = studentRepository;
            _universityRepository = universityRepository;
            _mapper = mapper;
            _certificateRepository = certificateRepository;
        }

        public PagingList<StudentViewModel> GetStudentList(int page)
        {
            var students = _studentRepository
               .GetAll()
               .Select(x => _mapper.Map<StudentViewModel>(x))
               .ToList();
            var model = PagingList.Create(students, 3, page);

            model.Action = "StudentListAndSearch";
            return model;
        }

        public PagingList<StudentViewModel> GetStudentListAndSearch(string searchBy, string searchStudent, int page = 1)
        {
            var query = from x in _studentRepository.GetAll() select x;
            if (!String.IsNullOrEmpty(searchStudent))
            {
                if (searchBy == "iin")
                {
                    query = query.Where(x => x.Iin.Equals(searchStudent));
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
                studentViewModels.Add(_mapper.Map<StudentViewModel>(item));
            }

            var model = PagingList.Create(studentViewModels, 3, page);
            model.RouteValue = new RouteValueDictionary {
                                    {"searchBy", searchBy},
                                    {"searchStudent", searchStudent} };

            model.Action = "StudentListAndSearch";
            return model;
        }

        public StudentViewModel GetStudentById(long studentId)
        {
            var student = _studentRepository.Get(studentId);
            var studentViewModel = _mapper.Map<StudentViewModel>(student);
            return studentViewModel;
        }

        public void GetStudentGrantByGpa(string select, double minGpaValue)
        {
            if (select == "issueGrant")
            {
                var studentsNoGrant = _studentRepository.GetAll()
                     .Where(x => x.IsGrant == false)
                     .Where(c => c.CourseYear > 1)
                     .Where(student => student.Gpa >= minGpaValue)
                     .ToList();
                foreach (var student in studentsNoGrant)
                {
                    _studentRepository.UpdateStudentGrantData(student.Id, true);
                }
            }
            else
            {
                var studentsYesGrant = _studentRepository.GetAll().Where(x => x.IsGrant == true).Where(student => student.Gpa <= minGpaValue).ToList();
                foreach (var student in studentsYesGrant)
                {
                    _studentRepository.UpdateStudentGrantData(student.Id, false);
                }
            }
        }

        public void GetStudentGrantIndividual(long id, bool isGrant)
        {
            _studentRepository.UpdateStudentGrantData(id, isGrant);
        }

        public void GetAddNewOrEditStudentAsync(StudentViewModel studentViewModel)
        {
            var student = _mapper.Map<Student>(studentViewModel);
            _studentRepository.Save(student);
        }

        public bool Remove(string iin)
        {
            var student = _studentRepository.GetStudentByIiN(iin);
            if (student == null)
            {
                return false;
            }

            _studentRepository.Remove(student);

            return true;
        }

        public List<Faculties> GetAllFaculties()
        {
            var allFaculties = Enum.GetValues(typeof(Faculties)).Cast<Faculties>().ToList();

            return allFaculties;
        }

        public List<University> GetUniversityList()
        {
            return _universityRepository.GetAll();
        }

        public University GetUniversityByUniversityName(string universityName)
        {
            return _universityRepository.GetUniversityByName(universityName);
        }

        public List<string> GetListOfUniversityNames()
        {
            var all = GetUniversityList();
            List<string> universityNames = new List<string>();
            foreach (var university in all)
            {
                universityNames.Add(university.Name);
            }

            return universityNames;
        }

        public List<long> GetListOfUniversityIds()
        {
            var all = GetUniversityList();
            List<long> universityIds = new List<long>();
            foreach (var university in all)
            {
                universityIds.Add(university.Id);
            }

            return universityIds;
        }

        public void EndStudyYearForUniversity()
        {
            List<Student> students = _studentRepository.GetAll();
            int fourthCourseStudentsCount = 0;
            foreach (Student student in students)
            {
                if (student.CourseYear != 4)
                {
                    student.CourseYear = student.CourseYear + 1;
                }
                else
                {
                    student.CourseYear = null;
                    student.GraduatedYear = DateTime.Now;
                    student.Certificates.Add(_certificateRepository.GetCertificateByType("High"));
                    fourthCourseStudentsCount++;
                }
                _studentRepository.Save(student);
            }
        }
    }
}
