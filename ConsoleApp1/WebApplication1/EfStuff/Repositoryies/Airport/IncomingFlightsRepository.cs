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
    }
}
