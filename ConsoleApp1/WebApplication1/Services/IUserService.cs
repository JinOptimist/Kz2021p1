using WebApplication1.EfStuff.Model;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Citizen GetUser();
        bool IsPolicmen();
        bool IsActiveDuty();
        bool IsSheriff();
        bool IsOfficer();
        bool IsTrainee();
        bool IsFireAdmin();
        bool IsHCWorker();
        bool IsTruckSpecialist();
    }
}