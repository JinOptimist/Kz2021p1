using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.Models.Television
{
    public class TvProgrammeShortViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Content Rating")]
        public ContentRating ContentRating { get; set; }

        [Display(Name = "Type Of Programme")]
        public TypeOfProgramme TypeOfProgramme { get; set; }
        public string AvatarUrl { get; set; }

        [Display(Name = "Avatar")]
        public IFormFile AvatarFile { get; set; }
    }
}
