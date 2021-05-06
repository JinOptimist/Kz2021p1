using System.Collections.Generic;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface IStudentRepository : IBaseRepository<Student>
    {
        Student GetStudentByIIN(string studentIIN);
        void UpdateStudentGrantData(long studentId, bool onGrant);
    }
}
