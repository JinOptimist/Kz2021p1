using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public enum TeamState
    {
        Free = 1,
        Busy = 2,
        [Display(Name = "Not at post")]
        NotWorking = 3
    }
}
