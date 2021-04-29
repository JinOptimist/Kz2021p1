using WebApplication1.EfStuff.Model;

namespace WebApplication1.Services
{
    public interface IUserService
    {
        Citizen GetUser();
        bool IsPolicment();
    }
}