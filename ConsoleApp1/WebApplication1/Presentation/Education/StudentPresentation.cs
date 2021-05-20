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
using WebApplication1.Models.Education;

namespace WebApplication1.Presentation
{
    public class StudentPresentation : IStudentPresentation
    {
        private IStudentRepository _studentRepository;
        private IUniversityRepository _universityRepository;
        private ICertificateRepository _certificateRepository;
        private IMapper _mapper;

        public const int MaxCourseYear = 4;
        public const int PageSize = 5;
        public const string CertificateTypeForHighEducation = "High";

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
               .OrderByDescending(x => x.CourseYear)               
               .ToList();
            var model = PagingList.Create(students, PageSize, page);

            model.Action = "StudentListAndSearch";
            return model;
        }

        public PagingList<StudentViewModel> GetStudentListAndSearch(string searchBy, string searchStudent, int page = 1)
        {
            var query = _studentRepository.GetAll().AsEnumerable();
            if (!String.IsNullOrEmpty(searchStudent))
            {
                switch (searchBy)
                {
                    case "iin":
                        query = query.Where(x => x.Iin == searchStudent);
                        break;
                    case "name":
                        query = query.Where(x => x.Name.Contains(searchStudent));
                        break;
                    case "courseYear":
                        query = query.Where(x => x.CourseYear == int.Parse(searchStudent)).OrderBy(s => s.University.Id);
                        break;
                    case "universityID":
                        query = query.Where(x => x.University.Id == int.Parse(searchStudent)).OrderBy(s => s.CourseYear);
                        break;
                    default:
                        break;
                }
            }

            var studentViewModels = query.Select(x => _mapper.Map<StudentViewModel>(x)).ToList();

            var model = PagingList.Create(studentViewModels, PageSize, page);
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
            bool isGrant = true;
            var students = _studentRepository.GetStudentsByGrantInfo(isGrant).Where(x => x.Gpa <= minGpaValue);

            if (select == "issueGrant")
            {
                isGrant = false;
                students = _studentRepository.GetStudentsByGrantInfo(isGrant).Where(x => x.Gpa >= minGpaValue);
            }

            foreach (var student in students)
            {
                student.IsGrant = !isGrant;
                _studentRepository.Save(student);
            }
        }

        public string GetStudentGrantIndividual(long id)
        {
            var student = _studentRepository.Get(id);
            string message;

            if (student.IsGrant)
            {
                student.IsGrant = false;
                message = $"Grant of student {student.Surname} {student.Name} {student.Patronymic}  was canceled ";
            }
            else
            {
                student.IsGrant = true;
                message = $"Grant was issued to student {student.Surname} {student.Name} {student.Patronymic}";
            }

            _studentRepository.Save(student);

            return message;
        }

        public void GetAddNewOrEditStudentAsync(StudentViewModel studentViewModel)
        {
            var student = _mapper.Map<Student>(studentViewModel);

            var university = GetUniversityByUniversityName(studentViewModel.University.Name);
            student.University = university;

            if (student.Id == 0)
            {
                var certificate = _certificateRepository.GetCertificateByType("Middle");
                student.Certificates.Add(certificate);
            }
            _studentRepository.Save(student);
        }

        public bool Remove(string iin)
        {
            var student = _studentRepository.GetStudentByIin(iin);
            if (student == null)
            {
                return false;
            }

            _studentRepository.Remove(student);

            return true;
        }
        public bool AddNewCertificate(string iin, string certificateType)
        {
            var student = _studentRepository.GetStudentByIin(iin);
            if (student == null)
            {
                return false;
            }
            var certificate = _certificateRepository.GetCertificateByType(certificateType);
            student.Certificates.Add(certificate);
            _studentRepository.Save(student);

            return true;
        }

        public bool CancelCertificate(string iin, string certificateType)
        {
            var student = _studentRepository.GetStudentByIin(iin);

            var certificate = _certificateRepository.GetCertificateByType(certificateType);

            if (certificate == null)
            {
                return false;
            }

            student.Certificates.Remove(certificate);
            _studentRepository.Save(student);

            return true;
        }

        public List<Faculties> GetAllFaculties()
        {
            var allFaculties = Enum.GetValues(typeof(Faculties)).Cast<Faculties>().ToList();

            return allFaculties;
        }

        public University GetUniversityByUniversityName(string universityName)
        {
            return _universityRepository.GetUniversityByName(universityName);
        }

        public List<string> GetListOfUniversityNames()
        {
            var universityNames = _universityRepository.GetUniversityNames();
            return universityNames;
        }

        public List<long> GetListOfUniversityIds()
        {
            var universityIds = _universityRepository.GetUniversityIds();
            return universityIds;
        }
               
        public List<string> GetListOfCertificateNames()
        {
            var certificateTypes = _certificateRepository.GetCertificateTypes();
            return certificateTypes;
        }

        public PagingList<StudentViewModel> GetStudentByFacultyAndCourseYear(string faculty, int courseYear, int page)
        {
            var query = new List<Student>();

            if (!String.IsNullOrEmpty(faculty))
            {
                query = _studentRepository.GetStudentsByFaculty(faculty);
            }
            if (courseYear > 0)
            {
                query = _studentRepository.GetStudentsByCourseYear(courseYear);
            }

            var studentViewModels = query.Select(x => _mapper.Map<StudentViewModel>(x)).ToList();

            var model = PagingList.Create(studentViewModels, PageSize, page);
            model.RouteValue = new RouteValueDictionary {
                                    {"faculty", faculty},
                                    {"courseYear", courseYear} };

            model.Action = "SearchStudentByFacultyAndCourseYears";
            return model;
        }

        public void EndStudyYearForUniversity()
        {
            var students = _studentRepository.GetAll();

            foreach (var student in students)
            {
                if (student.CourseYear < MaxCourseYear)
                {
                    student.CourseYear = student.CourseYear++;
                }
                else
                {
                    student.CourseYear = null;
                    student.Gpa = 0;
                    student.GraduatedYear = DateTime.Now;
                    student.Certificates.Add(_certificateRepository.GetCertificateByType(CertificateTypeForHighEducation));                    
                }
                _studentRepository.Save(student);
            }
        }
    }
}
