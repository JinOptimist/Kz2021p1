using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;
using WebApplication1.EfStuff.Model;
using WebApplication1.Models;

namespace WebApplication1.Presentation
{
    public interface IPupilPresentation
    {
        PagingList<PupilViewModel> GetPupilList(int page);
        PagingList<PupilViewModel> GetPupilListAndSearch(string searchBy, string searchPupil, int page);
        PupilViewModel GetPupilById(long pupilId);
        void GetAddNewOrEditPupil(PupilViewModel pupilViewModel);
        void GetPupilGrant(List<long> universityIds, int minValueForGrant);
        bool Remove(string iin);
        School GetSchoolBySchoolName(string schoolName);
        List<string> GetListOfSchoolNames();
        void EndStudyYearForSchool();
    }
}