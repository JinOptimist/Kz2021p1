﻿using AutoMapper;
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
                    query = query.Where(x => x.Iin.Equals(searchPupil));
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

            int randomForFacultyHashCode = rand.Next(1, allFaculties.Count());
            int index = rand.Next(0, universityIds.Count());

            var pupils = _pupilRepository.GetAll();
            foreach (var pupil in pupils)
            {
                /*if (pupil.ENT != null)
                {
                    StudentViewModel studentVIewModel = new StudentViewModel();
                    studentVIewModel.Iin = pupil.Iin;
                    studentVIewModel.Name = pupil.Name;
                    studentVIewModel.Surname = pupil.Surname;
                    studentVIewModel.Patronymic = pupil.Patronymic;
                    studentVIewModel.AvatarUrl = pupil.AvatarUrl;
                    studentVIewModel.Birthday = pupil.Birthday;
                    studentVIewModel.Email = pupil.Email;
                    studentVIewModel.Faculty = allFaculties.Where(x => x.GetHashCode() == randomForFacultyHashCode).SingleOrDefault().ToString();
                    studentVIewModel.CourseYear = 1;
                    studentVIewModel.Gpa = 2.67;
                    studentVIewModel.EnteredYear = DateTime.Now;
                    studentVIewModel.GraduatedYear = null;
                    studentVIewModel.UniversityId = universityIds.ElementAt(index); // Random()
                    //studentVIewModel.Certificates.Add(pupil.Certificate); //map

                    if (pupil.ENT >= minValueForGrant)
                    {
                        studentVIewModel.IsGrant = true;
                        var student = _mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                    else
                    {
                        studentVIewModel.IsGrant = false;
                        var student = _mapper.Map<Student>(studentVIewModel);
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                }*/

                if (pupil.ENT != null)
                {
                    Student student = new Student();
                    student.Iin = pupil.Iin;
                    student.Name = pupil.Name;
                    student.Surname = pupil.Surname;
                    student.Patronymic = pupil.Patronymic;
                    student.AvatarUrl = pupil.AvatarUrl;
                    student.Birthday = pupil.Birthday;
                    student.Email = pupil.Email;
                    student.Faculty = allFaculties.Where(x => x.GetHashCode() == randomForFacultyHashCode).SingleOrDefault().ToString();
                    student.CourseYear = 1;
                    student.Gpa = 2.67;
                    student.EnteredYear = DateTime.Now;
                    student.GraduatedYear = null;
                    student.UniversityId = universityIds.ElementAt(index);
                    student.Certificates.Add(pupil.Certificate);

                    if (pupil.ENT >= minValueForGrant)
                    {
                        student.IsGrant = true;                        
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                    else
                    {
                        student.IsGrant = false;
                        _studentRepository.Save(student);

                        _pupilRepository.Remove(pupil);
                    }
                }
            }
        }

        public bool Remove(string iin)
        {
            var pupil = _pupilRepository.GetPupilByIiN(iin);
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
                    pupil.Certificate = _certificateRepository.GetCertificateByType("Middle");
                }
                _pupilRepository.Save(pupil);
            }
        }
    }
}