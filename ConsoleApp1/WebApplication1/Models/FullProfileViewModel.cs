using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Localization;

namespace WebApplication1.Models
{
    public class FullProfileViewModel
    {
        [Display(Name = "FullProfile_Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public IFormFile AvatarFile { get; set; }

        [Display(Name = "FullProfile_Age", ResourceType = typeof(Resource))]
        public int Age { get; set; }
        
        public string Job { get; set; }

        public DateTime RegistrationDate { get; set; }

        public CompanyViewModel Company { get; set; }
    }
}
