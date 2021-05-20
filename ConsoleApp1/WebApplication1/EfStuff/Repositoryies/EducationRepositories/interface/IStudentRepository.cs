using System.Collections.Generic;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface IStudentRepository : IBaseRepository<Student>
    {
        Student GetStudentByIin(string studentIiN);
        List<Student> GetStudentsByFaculty(string faculty);
        List<Student> GetStudentsByCourseYear(int courseYear);
        List<Student> GetStudentsByGrantInfo(bool isGrant);
    }
}
