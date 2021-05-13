using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public enum FlightStatus
    {
        Departed = 0,
        OnTime = 1,
        Expected = 2,
        Landed = 3,
        Canceled = 4,
        Delayed = 5
    }
}
