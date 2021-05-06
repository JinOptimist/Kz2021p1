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
        private IMapper _mapper;

        public PupilPresentation(IPupilRepository pupilRepository, IStudentRepository studentRepository, 
            ISchoolRepository schoolRepository, IMapper mapper, IStudentPresentation studentPresentation)
        {
            _pupilRepository = pupilRepository;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _studentPresentation = studentPresentation;
        }

        public PagingList<PupilViewModel> GetPupilList(int page)
        {
            var pupils = _pupilRepository
                  .GetAll()
                  .Select(x => _mapper.Map<PupilViewModel>(x))
                  .ToList();
            var model = PagingList.Create(pupils, 3, page);
            model.Action = "PupilListAndSearch";
            return model;
        }
        public PagingList<PupilViewModel> GetPupilListAndSearch(string searchBy, string searchPupil, int page)
        {
            var query = from x in _pupilRepository.GetAll() select x;
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
            List<PupilViewModel> pupilViewModels = new List<PupilViewModel>();
            foreach (var item in query)
            {
                pupilViewModels.Add(_mapper.Map<PupilViewModel>(item));
            }
            var model = PagingList.Create(pupilViewModels, 3, page);
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

            _pupilRepository.Save(pupil);
        }

        public void GetPupilGrant(List<long> universityIds, int minValueForGrant)
        {
            var allFaculties = _studentPresentation.GetAllFaculties();
            Random rand = new Random();

            int randomForFacultyHashCode = rand.Next(0, allFaculties.Count());
            int index = rand.Next(0, universityIds.Count());

            var pupils = _pupilRepository.GetAll();
            foreach (var pupil in pupils)
            {
                if (pupil.ENT != null)
                {
                    StudentViewModel studentVIewModel = new StudentViewModel();
                    studentVIewModel.IIN = pupil.IIN;
                    studentVIewModel.Name = pupil.Name;
                    studentVIewModel.Surname = pupil.Surname;
                    studentVIewModel.Patronymic = pupil.Patronymic;
                    studentVIewModel.Avatar = pupil.Avatar;
                    studentVIewModel.Birthday = pupil.Birthday;
                    studentVIewModel.Email = pupil.Email;
                    studentVIewModel.Faculty = allFaculties.Where(x => x.GetHashCode() == randomForFacultyHashCode).SingleOrDefault().ToString();
                    studentVIewModel.CourseYear = 1;
                    studentVIewModel.Gpa = 2.67;
                    studentVIewModel.EnteredYear = DateTime.Now;
                    studentVIewModel.GraduatedYear = null;
                    studentVIewModel.UniversityId = universityIds.ElementAt(index); // Random()

                    if (pupil.ENT >= minValueForGrant)
                    {
                        studentVIewModel.OnGrant = true;
                        var student = _mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                    else
                    {
                        studentVIewModel.OnGrant = false;
                        var student = _mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                }
            }
        }
               
        public bool Remove(string iin)
        {
            var pupil = _pupilRepository.GetPupilByIIN(iin);
            if (pupil == null)
            {
                return false;
            }

            _pupilRepository.Remove(pupil);

            return true;
        }       

        public List<School> GetSchoolList()
        {
            return _schoolRepository.GetAll();
        }

        public School GetSchoolBySchoolName(string schoolName)
        {
            return _schoolRepository.GetSchoolByName(schoolName);
        }

        public List<string> GetListOfSchoolNames()
        {
            var all = GetSchoolList();
            List<string> schoolNames = new List<string>();
            foreach (var school in all)
            {
                schoolNames.Add(school.Name);
            }

            return schoolNames;
        }

        public void EndStudyYearForSchool()
        {
            List<Pupil> pupils = _pupilRepository.GetAll();
            Random rand = new Random();
            foreach (Pupil pupil in pupils)
            {
                if (pupil.ClassYear != 11)
                {
                    pupil.ClassYear = pupil.ClassYear + 1;
                }
                else
                {
                    pupil.ENT = rand.Next(50, 140);
                    pupil.ClassYear = null;
                    pupil.GraduatedYear = DateTime.Now;
                    // Certificate
                }
                _pupilRepository.Save(pupil);
            }
        }
    }
}
