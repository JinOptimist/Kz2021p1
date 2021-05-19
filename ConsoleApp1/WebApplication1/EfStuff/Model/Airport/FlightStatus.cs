using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Airport
{
    public enum FlightStatus
    {
        /// <summary>
        /// Flight was departed
        /// </summary>
        Departed = 0,
        /// <summary>
        /// Departing flight, waiting to be departed
        /// </summary>
        OnTime = 1,
        /// <summary>
        /// Incoming flight waiting to be admitted
        /// </summary>
        Expected = 2,
        /// <summary>
        /// Incoming flight after admission
        /// </summary>
        Landed = 3,
        /// <summary>
        /// Departing flight, that was canceled
        /// </summary>
        Canceled = 4,
        /// <summary>
        /// Delayed flight incoming or departing
        /// </summary>
        Delayed = 5
    }
}
