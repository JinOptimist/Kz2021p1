using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.Models.Television
{
    public class TvProgrammeViewModel
    {
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public ContentRating ContentRating { get; set; }
        [Required]
        public TypeOfProgramme TypeOfProgramme { get; set; }

        public string AvatarUrl { get; set; }
        [Required]
        public IFormFile AvatarFile { get; set; }

        public TvChannelViewModel Channel { get; set; }

        public List<TvProgrammeStaffViewModel> Staff { get; set; }
    }
}
