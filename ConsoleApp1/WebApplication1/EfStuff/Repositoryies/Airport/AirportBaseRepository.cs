using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public abstract class AirportBaseRepository<DbModel> where DbModel : FlightBaseModel
    {
        protected KzDbContext _kzDbContext;
        protected DbSet<DbModel> _dbSet;

        public AirportBaseRepository(KzDbContext kzDbContext)
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
            _dbSet.Add(model);
            _kzDbContext.SaveChanges();

            return model;
        }

        public void Remove(DbModel model)
        {
            _dbSet.Remove(model);
            _kzDbContext.SaveChanges();
        }
        public bool PutEntity(long id, DbModel flightInfoModel)
        {
            if (id != flightInfoModel.Id)
            {
                return false;
            }

            _kzDbContext.Entry(flightInfoModel).State = EntityState.Modified;

            try
            {
                _kzDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!flightInfoModelExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        private bool flightInfoModelExists(long id)
        {
            return _dbSet.Any(e => e.Id == id);
        }
    }
}
