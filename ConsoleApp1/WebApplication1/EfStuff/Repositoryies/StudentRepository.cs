using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Student GetStudentByIIN(string studentIIN)
        {        
            return _kzDbContext.Students.SingleOrDefault(x=>x.IIN.Equals(studentIIN));
        }

        public void UpdateStudentGrantData(long studentId, bool onGrant)
        {
            Student student = _kzDbContext.Students.SingleOrDefault(x => x.Id.Equals(studentId));
            student.OnGrant = onGrant;
            _kzDbContext.Students.Update(student);
            _kzDbContext.SaveChanges();
        }
    }
}
