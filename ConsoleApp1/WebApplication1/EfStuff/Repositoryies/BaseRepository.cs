using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public abstract class BaseRepository<DbModel> where DbModel : BaseModel
    {
        protected KzDbContext _kzDbContext;
        protected DbSet<DbModel> _dbSet;

        public BaseRepository(KzDbContext kzDbContext)
        {
            _kzDbContext = kzDbContext;
            _dbSet = _kzDbContext.Set<DbModel>();
        }

        public DbModel Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public List<DbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public DbModel Save(DbModel model)
        {
            if (model.Id > 0)
            {
                //_dbSet.Update(model);
                _kzDbContext.Entry(model).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Add(model);
            }
            
            _kzDbContext.SaveChanges();

            return model;
        }

        public void Remove(DbModel model)
        {
            _dbSet.Remove(model);
            _kzDbContext.SaveChanges();            
        }
    }
}
