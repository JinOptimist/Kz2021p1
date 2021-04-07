using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class SportEvent: BaseModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public string date { get; set; }
        [NotMapped]
        public IFormFile imagefile { get; set; }
    }
}
