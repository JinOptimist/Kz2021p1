using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvSchedule : BaseModel
    {
        public DateTime AiringTime { get; set; }
        public virtual TvProgramme Programme { get; set; }
    }
}
