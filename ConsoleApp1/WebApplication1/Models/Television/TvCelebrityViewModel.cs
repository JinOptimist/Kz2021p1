using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.Models.Television
{
    public class TvCelebrityViewModel
    {
        public string Name { get; set; }
        [Required]
        public CelebrityOccupation Occupation { get; set; }
        public virtual FullProfileViewModel Citizen { get; set; }
    }
}
