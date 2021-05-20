using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Television
{
    public class TvProgrammeCelebrityViewModel
    {
        [Required]
        public TvProgrammeShortViewModel Programme { get; set; }
        [Required]
        public TvCelebrityViewModel Celebrity { get; set; }
    }
}
