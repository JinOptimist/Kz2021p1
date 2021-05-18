using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Television
{
    public class TvScheduleViewModel
    {
        public long Id { get; set; }
        [Required]
        public DateTime AiringTime { get; set; }
        public TvProgrammeShortViewModel Programme { get; set; }
    }
}
