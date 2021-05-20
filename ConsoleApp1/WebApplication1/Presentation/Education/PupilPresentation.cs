using AutoMapper;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class PupilPresentation : IPupilPresentation
    {
        private IPupilRepository _pupilRepository;
        private IStudentRepository _studentRepository;
        private IStudentPresentation _studentPresentation;
        private ISchoolRepository _schoolRepository;
        private ICertificateRepository _certificateRepository;
        private IMapper _mapper;
        public const int MaxClassYear = 11;
        public const int MaxEntValue = 140;
        public const int MinEntValue = 50;
        public const int MinCourseYear = 1;
        public const int PageSize = 5;
        public const double Gpa = 3.67;
        public const string CertificateTypeForSecondaryEducation = "Middle";

        public PupilPresentation(IPupilRepository pupilRepository, IStudentRepository studentRepository,
            ISchoolRepository schoolRepository, IMapper mapper, IStudentPresentation studentPresentation,
            ICertificateRepository certificateRepository)
        {
            _pupilRepository = pupilRepository;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _studentPresentation = studentPresentation;
            _certificateRepository = certificateRepository;
        }

        public PagingList<PupilViewModel> GetPupilList(int page)
        {
            var pupils = _pupilRepository
                  .GetAll()
                  .Select(x => _mapper.Map<PupilViewModel>(x))
                  .ToList();
            var model = PagingList.Create(pupils, PageSize, page);
            model.Action = "PupilListAndSearch";
            return model;
        }
        public PagingList<PupilViewModel> GetPupilListAndSearch(string searchBy, string searchPupil, int page)
        {            
            var query = _pupilRepository.GetAll().AsEnumerable();

            if (!String.IsNullOrEmpty(searchPupil))
            {
                switch (searchBy)
                {
                    case "iin":
                        query = query.Where(x => x.Iin == searchPupil);
                        break;
                    case "name":
                        query = query.Where(x => x.Name.Contains(searchPupil));
                        break;
                    case "classYear":
                        query = query.Where(x => x.ClassYear == int.Parse(searchPupil)).OrderBy(s => s.School.Id);
                        break;
                    case "schoolID":
                        query = query.Where(x => x.School.Id == int.Parse(searchPupil)).OrderBy(s => s.ClassYear);
                        break;
                    default:
                        break;
                }
            }

            var pupilViewModels = query.Select(x => _mapper.Map<PupilViewModel>(x)).ToList();

            var model = PagingList.Create(pupilViewModels, PageSize, page);
            model.RouteValue = new RouteValueDictionary {
                                    {"searchBy", searchBy},
                                    {"searchPupil", searchPupil} };

            model.Action = "PupilListAndSearch";
            return model;
        }

        public PupilViewModel GetPupilById(long pupilId)
        {
            var pupil = _pupilRepository.Get(pupilId);
            var pupilViewModel = _mapper.Map<PupilViewModel>(pupil);
            return pupilViewModel;
        }

        public void GetAddNewOrEditPupil(PupilViewModel pupilViewModel)
        {
            var pupil = _mapper.Map<Pupil>(pupilViewModel);
            var school = GetSchoolBySchoolName(pupilViewModel.School.Name);
            pupil.School = school;

            _pupilRepository.Save(pupil);
        }

        public void GetPupilGrant(List<long> universityIds, int minValueForGrant)
        {
            var allFaculties = _studentPresentation.GetAllFaculties();
            Random rand = new Random();

            int randomForFacultyHashCode = rand.Next(1, allFaculties.Count());
            int index = rand.Next(0, universityIds.Count());

            var pupils = _pupilRepository.GetPupilsWithEnt();

            foreach (var pupil in pupils)
            {                
                var student = _mapper.Map<Student>(pupil);
                student.Id = 0;
                student.Faculty = allFaculties.Where(x => x.GetHashCode() == randomForFacultyHashCode).SingleOrDefault().ToString();
                student.CourseYear = MinCourseYear;
                student.Gpa = Gpa;
                student.EnteredYear = DateTime.Now;
                student.GraduatedYear = null;
                student.University.Id = universityIds.ElementAt(index);
                if(pupil.Certificate != null)
                {
                    student.Certificates.Add(pupil.Certificate);
                }              

                student.IsGrant = pupil.ENT >= minValueForGrant ? true : false;

                _studentRepository.Save(student);
                _pupilRepository.Remove(pupil);
            }
        }

        public bool Remove(string iin)
        {
            var pupil = _pupilRepository.GetPupilByIin(iin);
            if (pupil == null)
            {
                return false;
            }

            _pupilRepository.Remove(pupil);

            return true;
        }

        public School GetSchoolBySchoolName(string schoolName)
        {
            return _schoolRepository.GetSchoolByName(schoolName);
        }

        public List<string> GetListOfSchoolNames()
        {
            var schoolNames = _schoolRepository.GetSchoolNames();
            return schoolNames;
        }

        public void EndStudyYearForSchool()
        {
            var pupils = _pupilRepository.GetAll();
            Random rand = new Random();
            foreach (var pupil in pupils)
            {
                if (pupil.ClassYear < MaxClassYear)
                {
                    pupil.ClassYear = pupil.ClassYear++;
                }
                else
                {
                    pupil.ENT = rand.Next(MinEntValue, MaxEntValue);
                    pupil.ClassYear = null;
                    pupil.GraduatedYear = DateTime.Now;
                    pupil.Certificate = _certificateRepository.GetCertificateByType(CertificateTypeForSecondaryEducation);
                }
                _pupilRepository.Save(pupil);
            }
        }
    }
}
