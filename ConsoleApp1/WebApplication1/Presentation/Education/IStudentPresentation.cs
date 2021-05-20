using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Education;
using WebApplication1.Models;
using WebApplication1.Models.Education;

namespace WebApplication1.Presentation
{
    public interface IStudentPresentation
    {
        PagingList<StudentViewModel> GetStudentList(int page);
        PagingList<StudentViewModel> GetStudentListAndSearch(string searchBy, string searchStudent, int page = 1);
        StudentViewModel GetStudentById(long studentId);
        void GetStudentGrantByGpa(string select, double minGpaValue);
        string GetStudentGrantIndividual(long id);
        void GetAddNewOrEditStudentAsync(StudentViewModel studentViewModel);
        bool Remove(string iin);
        List<Faculties> GetAllFaculties();
        bool CancelCertificate(string iin, string certificateType);
        University GetUniversityByUniversityName(string universityName);
        List<string> GetListOfUniversityNames();
        List<long> GetListOfUniversityIds();        
        List<string> GetListOfCertificateNames();
        bool AddNewCertificate(string iin, string certificateType);
        PagingList<StudentViewModel> GetStudentByFacultyAndCourseYear(string faculty, int courseYear, int page);
        void EndStudyYearForUniversity();
    }
}