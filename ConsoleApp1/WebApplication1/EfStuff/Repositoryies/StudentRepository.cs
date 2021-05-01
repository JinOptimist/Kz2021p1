using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class StudentRepository : BaseRepository<Student>, IStudentRepository
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
