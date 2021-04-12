using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class PassengersRepository
    {
        protected KzDbContext _kzDbContext;

        public PassengersRepository(KzDbContext kzDbContext)
        {
            _kzDbContext = kzDbContext;
        }

        public Passenger Get(long id)
        {
            return _kzDbContext.Passengers.SingleOrDefault(x => x.Id == id);
        }

        public List<Passenger> GetAll()
        {
            return _kzDbContext.Passengers.ToList();
        }

        public Passenger Save(Passenger model)
        {
            _kzDbContext.Passengers.Add(model);
            _kzDbContext.SaveChanges();

            return model;
        }

        public void Remove(Passenger model)
        {
            _kzDbContext.Passengers.Remove(model);
            _kzDbContext.SaveChanges();
        }
        public bool PutEntity(long id, Passenger passengerModel)
        {
            if (id != passengerModel.Id)
            {
                return false;
            }

            _kzDbContext.Entry(passengerModel).State = EntityState.Modified;

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
            return _kzDbContext.Passengers.Any(e => e.Id == id);
        }
    }
}
