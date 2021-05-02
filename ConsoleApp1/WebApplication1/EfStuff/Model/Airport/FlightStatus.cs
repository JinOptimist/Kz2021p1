using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public enum FlightStatus
    {
        Departed,
        OnTime,
        Expected,
        Landed,
        Canceled,
        Delayed
    }
}
