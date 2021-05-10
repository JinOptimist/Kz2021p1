using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public abstract class PersonViewModel
    {
        public long Id { get; set; }
        public string Iin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AvatarUrl { get; set; }
        public IFormFile AvatarFile { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
    }
}
