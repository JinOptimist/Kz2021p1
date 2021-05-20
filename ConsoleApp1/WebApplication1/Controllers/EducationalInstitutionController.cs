using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EducationalInstitutionController : Controller
    {
        private IUniversityRepository _universityRepository;
        private ISchoolRepository _schoolRepository;
        private IMapper Mapper { get; set; }

        public EducationalInstitutionController(IUniversityRepository universityRepository, ISchoolRepository schoolRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _schoolRepository = schoolRepository;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UniversityList()
        {
            var universities = _universityRepository
               .GetAll()
               .Select(x => Mapper.Map<UniversityViewModel>(x))
               .ToList();
            return View(universities);
        }
        public IActionResult UniversityFullInfo(int IDUniversity)
        {
            var university = _universityRepository.Get(IDUniversity);
            var universityViewModel = Mapper.Map<UniversityViewModel>(university);
            universityViewModel.StudentCount = university.Students.Count();
            return View(universityViewModel);
        }

        public IActionResult SchoolList()
        {
            var schools = _schoolRepository
               .GetAll()
               .Select(x => Mapper.Map<SchoolViewModel>(x))
               .ToList();           
            return View(schools);
        }
        public IActionResult SchoolFullInfo(int IDSchool)
        {
            var school = _schoolRepository.Get(IDSchool);
            var schoolViewModel = Mapper.Map<SchoolViewModel>(school);
            schoolViewModel.PupilCount = school.Pupils.Count();
            return View(schoolViewModel);
        }
    }
}
