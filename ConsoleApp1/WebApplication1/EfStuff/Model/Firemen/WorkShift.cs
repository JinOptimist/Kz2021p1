using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public enum WorkShift
    {
        [Display(Name = "6 A.M. - 14 P.M")]
        Morning = 1,
        [Display(Name = "14 P.M. - 22 P.M")]
        Day = 2,
        [Display(Name = "22 P.M. - 6 A.M")]
        Night = 3
    }
}
