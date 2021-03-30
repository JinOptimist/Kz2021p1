using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class DepartingFlightsRepository : AirportBaseRepository<DepartingFlightInfo>
    {
        public DepartingFlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public bool PutEntity(long id, DepartingFlightInfo departingFlightInfo)
        {
            if (id != departingFlightInfo.Id)
            {
                return false;
            }

            _kzDbContext.Entry(departingFlightInfo).State = EntityState.Modified;

            try
            {
                _kzDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartingFlightInfoExists(id))
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
        private bool DepartingFlightInfoExists(long id)
        {
            return _kzDbContext.DepartingFlightsInfo.Any(e => e.Id == id);
        }
    }
}
