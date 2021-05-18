using WebApplication1.EfStuff.Model;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Citizen GetUser();
        bool IsPolicment();
        bool IsHCWorker();
        bool IsTvStaff();
        bool IsTvAdmin();
        bool IsTvDirector();
        bool IsTvCastingDirector();
    }
}