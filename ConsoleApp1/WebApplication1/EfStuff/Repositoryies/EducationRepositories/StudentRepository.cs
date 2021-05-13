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
            return _kzDbContext.Students.SingleOrDefault(x => x.Iin.Equals(studentIin));
        }

        public List<Student> GetStudentsByFaculty(string faculty)
        {
            return _kzDbContext.Students.Where(x => x.Faculty.Equals(faculty)).ToList();
        }

        public List<Student> GetStudentsByCourseYear(int courseYear)
        {
            return _kzDbContext.Students.Where(x => x.CourseYear == courseYear).ToList();
        }

        public void UpdateStudentGrantData(long studentId, bool isGrant)
        {
            Student student = _kzDbContext.Students.SingleOrDefault(x => x.Id == studentId);
            student.IsGrant = isGrant;
            _kzDbContext.Students.Update(student);
            _kzDbContext.SaveChanges();
        }
    }
}
