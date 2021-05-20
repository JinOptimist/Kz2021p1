using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Television
{
    public class TvChannelViewModel
    {
        public long Id { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Working from")]
        public DateTime WorkingFrom { get; set; }

        [Display(Name ="Staff number")]
        public int StaffCount { get; set; }

        [Display(Name = "Programme number")]
        public int ProgrammeCount { get; set; }
    }
}
