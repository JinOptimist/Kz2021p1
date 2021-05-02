using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        DbModel Get(long id);
        List<DbModel> GetAll();
        void Remove(DbModel model);
        DbModel Save(DbModel model);
        IQueryable<DbModel> GetAllAsIQueryable();
    }
}