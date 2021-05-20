using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.Models.Television
{
    public class TvStaffViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select a role")]
        public Occupation Occupation { get; set; }
        public FullProfileViewModel Citizen { get; set; }
        public TvChannelViewModel Channel { get; set; }
    }
}
