using AutoMapper;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public class PupilPresentation
    {
        private PupilRepository _pupilRepository;
        private StudentRepository _studentRepository;
        private IMapper Mapper { get; set; }

        public PupilPresentation(PupilRepository pupilRepository, StudentRepository studentRepository, IMapper mapper)
        {
            _pupilRepository = pupilRepository;
            _studentRepository = studentRepository;
            Mapper = mapper;           
        }

        public List<PupilViewModel> GetPupilList()
        {
            var pupils = _pupilRepository
                  .GetAll()
                  .Select(x => Mapper.Map<PupilViewModel>(x))
                  .ToList();
            return pupils;
        }

        public List<PupilViewModel> GetPupilListAndSearch(string searchBy, string searchPupil)
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
            List<PupilViewModel> pupilViewModel = new List<PupilViewModel>();
            foreach (var item in query)
            {
                pupilViewModel.Add(Mapper.Map<PupilViewModel>(item));
            }
            return pupilViewModel;
        }

        public PupilViewModel GetPupilFullInfo(string IINPupil)
        {
            var pupil = _pupilRepository.GetPupilByIIN(IINPupil);
            var pupilViewModel = Mapper.Map<PupilViewModel>(pupil);
            return pupilViewModel;
        }

        public void GetAddNewPupil(PupilViewModel pupilViewModel)
        {
            var pupil = Mapper.Map<Pupil>(pupilViewModel);

            _pupilRepository.Save(pupil);
        }

        public void GetPupilGrant(int minValueForGrant)
        {
            // добавить вытаскивание айди универа рандомно
            // enum faculty добавить и тоже рандомно присвоивать при выдаче гранта и 
            // регистрации ученика в качестве студента

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
                    studentVIewModel.Birthday = pupil.Birthday;
                    studentVIewModel.Email = pupil.Email;
                    studentVIewModel.Faculty = "Biology";
                    studentVIewModel.CourseYear = 1;
                    studentVIewModel.Gpa = 2.67;
                    studentVIewModel.EnteredYear = DateTime.Now;
                    studentVIewModel.GraduatedYear = null;
                    studentVIewModel.UniversityId = 100; // Random()

                    if (pupil.ENT >= minValueForGrant)
                    {
                        studentVIewModel.OnGrant = true;
                        var student = Mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                    else
                    {
                        studentVIewModel.OnGrant = false;
                        var student = Mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                }
            }
        }
    }
}
