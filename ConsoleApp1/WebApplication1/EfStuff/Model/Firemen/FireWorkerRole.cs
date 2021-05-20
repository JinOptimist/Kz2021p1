using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Firemen
{
    public enum FireWorkerRole
    {
        [Display(Name = "Fire Admin")]
        Fireadmin = 1,
        [Display(Name = "Fireman Worker")]
        FiremanWorker = 2,
        [Display(Name = "FireTruck Specialist")]
        FireTruckSpecialist = 3,
    }
}
