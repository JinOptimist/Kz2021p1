using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class CitizenRepository
    {
        private KzDbContext _kzDbContext;

        public CitizenRepository(KzDbContext kzDbContext)
        {
            _kzDbContext = kzDbContext;
        }

        public Citizen Get(long id)
        {
            return _kzDbContext.Citizens.SingleOrDefault(x => x.Id == id);
        }

        public Citizen GetByName(string name)
        {
            return _kzDbContext.Citizens.SingleOrDefault(x => x.Name == name);
        }

        public List<Citizen> GetAll()
        {
            return _kzDbContext.Citizens.ToList();
        }

        public Citizen Save(Citizen citizen)
        {
            _kzDbContext.Citizens.Add(citizen);
            _kzDbContext.SaveChanges();
            
            return citizen;
        }

        public void Remove(Citizen citizen)
        {
            _kzDbContext.Citizens.Remove(citizen);
            _kzDbContext.SaveChanges();
        }

    }
}
