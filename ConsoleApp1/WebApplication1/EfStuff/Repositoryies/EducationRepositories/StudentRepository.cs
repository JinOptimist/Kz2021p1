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

        public Student GetStudentByIiN(string studentIin)
        {
            return _kzDbContext.Students.SingleOrDefault(x => x.Iin.Equals(studentIin));
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
