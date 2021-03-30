using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;

namespace WebApplication1.EfStuff.Repositoryies.Airport
{
    public class IncomingFlightsRepository : AirportBaseRepository<IncomingFlightInfo>
    {
        public IncomingFlightsRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public bool PutEntity(long id, IncomingFlightInfo incomingFlightInfo)
        {
            if (id != incomingFlightInfo.Id)
            {
                return false;
            }

            _kzDbContext.Entry(incomingFlightInfo).State = EntityState.Modified;

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
