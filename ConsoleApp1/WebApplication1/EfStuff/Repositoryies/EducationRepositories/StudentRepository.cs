using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Student GetStudentByIin(string studentIin)
        {
            return _kzDbContext.Students.SingleOrDefault(x => x.Iin == studentIin);
        }

        public List<Student> GetStudentsByFaculty(string faculty)
        {
            return _kzDbContext.Students.Where(x => x.Faculty == faculty).ToList();
        }

        public List<Student> GetStudentsByCourseYear(int courseYear)
        {
            return _kzDbContext.Students.Where(x => x.CourseYear == courseYear).ToList();
        }

        public List<Student> GetStudentsByGrantInfo(bool isGrant)
        {
            return _kzDbContext.Students.Where(x => x.IsGrant == isGrant).Where(x => x.Gpa > 0).ToList();
        }
    }
}
