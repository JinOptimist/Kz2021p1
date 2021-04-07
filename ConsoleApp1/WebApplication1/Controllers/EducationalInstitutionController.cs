using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EducationalInstitutionController : Controller
    {
        private UniversityRepository UniversityRepository { get; set; }
        private SchoolRepository SchoolRepository { get; set; }
        private IMapper Mapper { get; set; }

        public EducationalInstitutionController(UniversityRepository universityRepository, SchoolRepository schoolRepository, IMapper mapper)
        {
            UniversityRepository = universityRepository;
            SchoolRepository = schoolRepository;
            Mapper = mapper;
        }

        public IActionResult UniversityList()
        {
            var universities = UniversityRepository
               .GetAll()
               .Select(x => Mapper.Map<UniversityViewModel>(x))
               .ToList();
            return View(universities);
        }
        public IActionResult UniversityFullInfo(int IDUniversity)
        {
            var university = UniversityRepository.Get(IDUniversity);
            var universityViewModel = Mapper.Map<UniversityViewModel>(university);
            return View(universityViewModel);
        }

        public IActionResult SchoolList()
        {
            var schools = SchoolRepository
               .GetAll()
               .Select(x => Mapper.Map<SchoolViewModel>(x))
               .ToList();
            return View(schools);
        }
        public IActionResult SchoolFullInfo(int IDSchool)
        {
            var school = SchoolRepository.Get(IDSchool);
            var schoolViewModel = Mapper.Map<SchoolViewModel>(school);
            return View(schoolViewModel);
        }
    }
}
