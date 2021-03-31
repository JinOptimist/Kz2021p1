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
    }
}
