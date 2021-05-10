using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public interface IStudentPresentation
    {
        PagingList<StudentViewModel> GetStudentList(int page);
        PagingList<StudentViewModel> GetStudentListAndSearch(string searchBy, string searchStudent, int page = 1);
        StudentViewModel GetStudentById(long studentId);
        void GetStudentGrantByGpa(string select, double minGpaValue);
        void GetStudentGrantIndividual(long id, bool isGrant);
        void GetAddNewOrEditStudentAsync(StudentViewModel studentViewModel);
        bool Remove(string iin);
        List<Faculties> GetAllFaculties();
        List<University> GetUniversityList();
        University GetUniversityByUniversityName(string universityName);
        List<string> GetListOfUniversityNames();
        List<long> GetListOfUniversityIds();
        void EndStudyYearForUniversity();
    }
}